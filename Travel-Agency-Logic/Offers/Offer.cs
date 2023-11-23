using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Offers;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Offers
{
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

            if (await _context.Set<T>().AnyAsync(o => o.Name == offer.Name))
                return new BadRequest<IdResponse>("The offer already exists");

            if (!CheckValidity(offer))
                return new BadRequest<IdResponse>("The offer is not valid");

            var entity = offer.Offer(agencyId);
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

            var package = new PackageRequest
                { Description = offer.Description, Discount = 0, Offers = new List<int> { entity.Id } };

            await _packageService.CreatePackage(package, user);

            return new ApiResponse<IdResponse>(new IdResponse { Id = entity.Id });
        }

        public async Task<ApiResponse> UpdateOffer(int id, OfferRequest<T> offerRequest, UserBasic user)
        {
            var offer = await _context.Set<T>().FindAsync(id);
            if (offer is null)
                return new NotFound("Offer not found");

            var check = await CheckPermissions(user, offer.AgencyId);
            if (!check.Ok)
                return check.ConvertApiResponse();

            if (!CheckValidity(offerRequest))
                return new BadRequest("The offer is not valid");

            if (!await CheckDependency(id))
                return new BadRequest("The offer is in use");

            var newOffer = offerRequest.Offer(offer.AgencyId, offer);

            _context.Update(newOffer);
            await _context.SaveChangesAsync();

            return new ApiResponse();
        }

        public async Task<ApiResponse> DeleteOffer(int id, UserBasic user)
        {
            var offer = await _context.Set<T>().FindAsync(id);

            if (offer is null)
                return new NotFound("Offer not found");

            var check = await CheckPermissions(user, offer.AgencyId);
            if (!check.Ok)
                return check.ConvertApiResponse();

            if (!await CheckDependency(id))
                return new BadRequest("The offer is in use");

            _context.Set<T>().Remove(offer);
            await _context.SaveChangesAsync();

            return new ApiResponse();
        }

        private async Task<ApiResponse<int>> CheckPermissions(UserBasic user, int agencyId = -1)
        {
            if (!(user.Role == Roles.AdminAgency || user.Role == Roles.ManagerAgency))
                return new Unauthorized<int>("You don't have permissions");

            var fullUser = await _context.UserAgencies.FindAsync(user.Id);

            if (fullUser is null) return new Unauthorized<int>("You don't have permissions");

            if (agencyId != -1 && fullUser.AgencyId != agencyId)
                return new Unauthorized<int>("You don't have permissions");

            return new ApiResponse<int>(fullUser.AgencyId);
        }


        private static bool CheckValidity(OfferRequest<T> offer) =>
            offer.Availability >= 0 && offer.Price >= 0 && offer.StartDate <= offer.EndDate &&
            Helpers.ValidDate(offer.StartDate);

        private async Task<bool> CheckDependency(int id)
        {
            if (await this._context.Reserves.Include(r => r.Package).ThenInclude(p => p.Offers)
                    .AnyAsync(r => r.Package.Offers.Select(o => o.Id).Contains(id)))
                return false;

            return true;
        }
    }
}