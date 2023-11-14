using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Offers;
using Travel_Agency_Domain.Packages;
using Travel_Agency_Domain.Payments;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Offers
{
    public abstract class ReserveService<T1, T2> : IReserveService<T1, T2> where T1 : Reserve where T2 : Payment
    {
        private readonly TravelAgencyContext _context;

        public ReserveService(TravelAgencyContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IdResponse>> CreateReserve(ReserveRequest<T1, T2> request, UserBasic userBasic)
        {
            if (!CheckPermissions(userBasic)) 
                return new Unauthorized<IdResponse>("You don't have permissions");

            var package = await _context.Packages.FirstOrDefaultAsync(p => p.Id == request.PackageId);
            if (package is null) 
                return new NotFound<IdResponse>("Package not found");

            if (!await CheckAvailability(package))
                return new BadRequest<IdResponse>("There is no availability for this package");

            var price = package.Price();

            var payment = request.Payment(price);
            _context.Set<T2>().Add(payment);
            await _context.SaveChangesAsync();

            var reserve = request.Reserve(payment.Id);
            _context.Set<T1>().Add(reserve);
            await _context.SaveChangesAsync();

            return new ApiResponse<IdResponse>(new IdResponse { Id = reserve.Id });
        }

        private async Task<bool> CheckAvailability(Package package)
        {
            foreach (Offer offer in package.Offers)
            {
                var count = await _context.Reserves.Where(r => r.Package.Offers
                    .Select(o => o.Id).Contains(offer.Id)).CountAsync();
                if (count >= offer.Availability) return false;
            }

            return true;
        }

        internal abstract bool CheckPermissions(UserBasic user);
    }
}