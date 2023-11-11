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
            if (!await CheckPermissions(user, offer.AgencyId))
                return new Unauthorized<IdResponse>("You don't have permissions");

            if (await _context.Set<T>().AnyAsync(o => o.Name == offer.Name))
                return new BadRequest<IdResponse>("The offer already exists");

            if (!await _context.Agencies.AnyAsync(a => a.Id == offer.AgencyId))
                return new BadRequest<IdResponse>("The agency don't exists");

            if (!CheckValidity(offer))
                return new BadRequest<IdResponse>("The offer is not valid");

            var entity = offer.Offer();
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

            var package = new PackageRequest
                { Description = offer.Description, Discount = 0, Offers = new List<int> { entity.Id } };

            await _packageService.CreatePackage(package, user);

            return new ApiResponse<IdResponse>(new IdResponse { Id = entity.Id });
        }

        public async Task<ApiResponse> UpdateOffer(int id, OfferRequest<T> offer, UserBasic user)
        {
            if (!await CheckPermissions(user, offer.AgencyId))
                return new Unauthorized("You don't have permissions");

            if (!await _context.Set<T>().AnyAsync(o => o.Id == id))
                return new NotFound("Offer not found");

            if (!CheckValidity(offer))
                return new BadRequest("The offer is not valid");

            var newOffer = offer.Offer();
            newOffer.Id = id;

            _context.Update(newOffer);
            await _context.SaveChangesAsync();

            return new ApiResponse();
        }

        public async Task<ApiResponse> DeleteOffer(int id, UserBasic user)
        {
            var offer = await _context.Set<T>().FindAsync(id);

            if (offer is null)
                return new NotFound("Offer not found");

            if (!await CheckPermissions(user, offer.AgencyId))
                return new Unauthorized("You don't have permissions");

            _context.Set<T>().Remove(offer);
            await _context.SaveChangesAsync();

            return new ApiResponse();
        }

        private async Task<bool> CheckPermissions(UserBasic user, int agencyId)
        {
            if (!(user.Role == Roles.AdminAgency || user.Role == Roles.ManagerAgency)) return false;

            var fullUser = await _context.UserAgencies.FindAsync(user.Id);

            if (fullUser is null) return false;

            return fullUser.AgencyId == agencyId;
        }


        private static bool CheckValidity(OfferRequest<T> offer) =>
            offer.Availability >= 0 && offer.Price >= 0 && offer.StartDate <= offer.EndDate &&
            Helpers.ValidDate(offer.StartDate);
    }
}