using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Offers;
using Travel_Agency_Domain.Services;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Services
{
    public class OfferService<T> : IOfferService<T> where T : Offer
    {
        private readonly TravelAgencyContext _context;

        public OfferService(TravelAgencyContext context)
        {
            _context = context;
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

            _context.Set<T>().Add(offer.Offer());
            await _context.SaveChangesAsync();

            return new ApiResponse<IdResponse>((await this._context.Set<T>()
                .Where(o => o.Name == offer.Name).Select(o => new IdResponse { Id = o.Id })
                .SingleOrDefaultAsync())!);
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
            offer.Availability >= 0 && offer.Price >= 0 && offer.StartDate <= offer.EndDate;
    }
}