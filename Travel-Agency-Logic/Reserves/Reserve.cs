using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Packages;
using Travel_Agency_Domain.Payments;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Reserves;

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

        var package = await _context.Packages.Include(x => x.FlightOffers).Include(x => x.HotelOffers)
            .Include(x => x.ExcursionOffers).FirstOrDefaultAsync(p => p.Id == request.PackageId);
        if (package is null)
            return new NotFound<IdResponse>("Package not found");

        if (!await CheckAvailability(package, request.UserIdentities.Count))
            return new BadRequest<IdResponse>("There is no availability for this package");

        if (!Helpers.ValidDate(package.StartDate()))
            return new BadRequest<IdResponse>($"The {(package.IsSingleOffer ? "offer" : "package")} has expired");

        var price = package.Price();

        var payment = request.Payment(price);
        _context.Set<T2>().Add(payment);
        await _context.SaveChangesAsync();

        var reserve = request.Reserve(payment.Id, userBasic.Id);
        AddOffers(package, reserve);

        _context.Set<T1>().Add(reserve);
        await _context.SaveChangesAsync();

        return new ApiResponse<IdResponse>(new IdResponse { Id = reserve.Id });
    }

    private async Task<bool> CheckAvailability(Package package, int cant)
    {
        foreach (var offer in package.HotelOffers)
        {
            var count = await _context.Reserves.Where(r => r.Package.HotelOffers
                .Select(o => o.Id).Contains(offer.Id)).CountAsync();
            if (count + cant > offer.Availability) return false;
        }

        foreach (var offer in package.ExcursionOffers)
        {
            var count = await _context.Reserves.Where(r => r.Package.ExcursionOffers
                .Select(o => o.Id).Contains(offer.Id)).CountAsync();
            if (count + cant > offer.Availability) return false;
        }

        foreach (var offer in package.FlightOffers)
        {
            var count = await _context.Reserves.Where(r => r.Package.FlightOffers
                .Select(o => o.Id).Contains(offer.Id)).CountAsync();
            if (count + cant > offer.Availability) return false;
        }

        return true;
    }

    private void AddOffers(Package package, T1 reserve)
    {
        foreach (var offer in package.HotelOffers)
            reserve.Offers.Add(offer);
        foreach (var offer in package.FlightOffers)
            reserve.Offers.Add(offer);
        foreach (var offer in package.ExcursionOffers)
            reserve.Offers.Add(offer);
    }

    internal abstract bool CheckPermissions(UserBasic user);
}