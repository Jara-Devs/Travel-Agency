using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_Core.Enums;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Offers;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Offers;

public class OfferService<T> : IOfferService<T> where T : Offer
{
    private readonly TravelAgencyContext _context;

    private readonly IPackageService _packageService;

    public OfferService(TravelAgencyContext context, IPackageService packageService)
    {
        _context = context;
        _packageService = packageService;
    }

    public async Task<ApiResponse<IdResponse>> CreateOffer(OfferRequest<T> offer, UserBasic user)
    {
        var check = await CheckPermissions(user);
        if (!check.Ok)
            return check.ConvertApiResponse<IdResponse>();

        var agencyId = check.Value;

        if (await _context.Offers.AnyAsync(o => o.Name == offer.Name))
            return new BadRequest<IdResponse>("The offer already exists");

        if (await _context.Packages.AnyAsync(p => p.Name == offer.Name))
            return new BadRequest<IdResponse>("The package whit the same name already exists");

        if (!CheckValidity(offer))
            return new BadRequest<IdResponse>("The offer is not valid");

        var response = await BuildRequest(offer, agencyId);
        if (!response.Ok) return response.ConvertApiResponse<IdResponse>();

        var entity = response.Value!;
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();

        var hotelOffers = new List<Guid>();
        var excursionOffers = new List<Guid>();
        var flightOffers = new List<Guid>();

        if (offer is HotelOfferRequest)
            hotelOffers.Add(entity.Id);
        else if (offer is ExcursionOfferRequest)
            excursionOffers.Add(entity.Id);
        else
            flightOffers.Add(entity.Id);

        var package = new PackageRequest
        {
            Description = offer.Description, Discount = 0, Name = offer.Name,
            HotelOffers = hotelOffers, ExcursionOffers = excursionOffers, FlightOffers = flightOffers
        };

        await _packageService.CreatePackage(package, user, true);

        return new ApiResponse<IdResponse>(new IdResponse { Id = entity.Id });
    }

    public async Task<ApiResponse> UpdateOffer(Guid id, OfferRequest<T> offerRequest, UserBasic user)
    {
        var offer = await _context.Set<T>().Include(x => x.Facilities).FirstOrDefaultAsync(x => x.Id == id);
        if (offer is null)
            return new NotFound("Offer not found");

        var check = await CheckPermissions(user, offer.AgencyId);
        if (!check.Ok)
            return check.ConvertApiResponse();

        if (await _context.Offers.AnyAsync(o => o.Name == offerRequest.Name && o.Id != offer.Id))
            return new BadRequest("The offer already exists");
        if (await _context.Packages.AnyAsync(p => p.Name == offerRequest.Name && !p.IsSingleOffer))
            return new BadRequest("The package whit the same name already exists");

        if (!CheckValidity(offerRequest))
            return new BadRequest("The offer is not valid");

        if (!await CheckDependency(id))
            return new BadRequest("The offer is in use");
        
        var package = await _context.Packages.Where(x => x.Name == offer.Name).FirstOrDefaultAsync();
        if (package is null) return new BadRequest("The offer is not a single offer");

        var response = await BuildRequest(offerRequest, offer.AgencyId, offer);
        if (!response.Ok) return response.ConvertApiResponse();
        
        package.Name= offerRequest.Name;
        
        _context.Packages.Update(package);
        _context.Update(response.Value!);
        await _context.SaveChangesAsync();

        return new ApiResponse();
    }

    public async Task<ApiResponse> DeleteOffer(Guid id, UserBasic user)
    {
        var offer = await _context.Set<T>().FindAsync(id);

        if (offer is null)
            return new NotFound("Offer not found");

        var check = await CheckPermissions(user, offer.AgencyId);
        if (!check.Ok)
            return check.ConvertApiResponse();

        if (!await CheckDependency(id))
            return new BadRequest("The offer is in use");

        var package = await _context.Packages.Where(x => x.Name == offer.Name).FirstOrDefaultAsync();

        if (package is null) return new BadRequest("The offer is not a single offer");

        _context.Set<T>().Remove(offer);
        _context.Packages.Remove(package);
        await _context.SaveChangesAsync();

        return new ApiResponse();
    }

    private async Task<ApiResponse<Guid>> CheckPermissions(UserBasic user, Guid? agencyId = null)
    {
        if (!(user.Role == Roles.AdminAgency || user.Role == Roles.ManagerAgency))
            return new Unauthorized<Guid>("You don't have permissions");

        var fullUser = await _context.UserAgencies.FindAsync(user.Id);

        if (fullUser is null) return new Unauthorized<Guid>("You don't have permissions");

        if (agencyId is not null && fullUser.AgencyId != agencyId)
            return new Unauthorized<Guid>("You don't have permissions");

        return new ApiResponse<Guid>(fullUser.AgencyId);
    }


    private static bool CheckValidity(OfferRequest<T> offer)
    {
        return offer.Availability >= 0 && offer.Price >= 0 && offer.StartDate <= offer.EndDate &&
               Helpers.ValidDate(offer.StartDate);
    }

    private async Task<bool> CheckDependency(Guid id)
    {
        if (await _context.Packages.Include(p => p.HotelOffers).Where(p => !p.IsSingleOffer)
                .AnyAsync(p => p.HotelOffers.Select(o => o.Id).Contains(id)))
            return false;
        if (await _context.Packages.Include(p => p.FlightOffers).Where(p => !p.IsSingleOffer)
                .AnyAsync(p => p.FlightOffers.Select(o => o.Id).Contains(id)))
            return false;
        if (await _context.Packages.Include(p => p.ExcursionOffers).Where(p => !p.IsSingleOffer)
                .AnyAsync(p => p.ExcursionOffers.Select(o => o.Id).Contains(id)))
            return false;

        if (await _context.Reserves.Include(r => r.Package).ThenInclude(p => p.HotelOffers)
                .AnyAsync(r => r.Package.HotelOffers.Select(o => o.Id).Contains(id)))
            return false;
        if (await _context.Reserves.Include(r => r.Package).ThenInclude(p => p.ExcursionOffers)
                .AnyAsync(r => r.Package.ExcursionOffers.Select(o => o.Id).Contains(id)))
            return false;
        if (await _context.Reserves.Include(r => r.Package).ThenInclude(p => p.FlightOffers)
                .AnyAsync(r => r.Package.FlightOffers.Select(o => o.Id).Contains(id)))
            return false;

        return true;
    }

    private async Task<ApiResponse<T>> BuildRequest(OfferRequest<T> request, Guid agencyId, T? entity = null)
    {
        var hotel = false;
        var excursion = false;
        var flight = false;

        entity = request.Offer(agencyId, entity);
        entity.Facilities.Clear();

        foreach (var item in request.Facilities)
        {
            var facility = await _context.Facilities.FindAsync(item);
            if (facility is null)
                return new BadRequest<T>("Not found facility");

            hotel = hotel || facility.Type == FacilityType.Hotel;
            excursion = excursion || facility.Type == FacilityType.Excursion;
            flight = flight || facility.Type == FacilityType.Flight;

            entity.Facilities.Add(facility);
        }

        if (hotel && entity is not HotelOffer)
            return new BadRequest<T>("The offer is not a hotel offer");
        if (excursion && entity is not ExcursionOffer)
            return new BadRequest<T>("The offer is not a excursion offer");
        if (flight && entity is not FlightOffer)
            return new BadRequest<T>("The offer is not a flight offer");

        return new ApiResponse<T>(entity);
    }
}