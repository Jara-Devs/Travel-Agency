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
    protected readonly TravelAgencyContext Context;

    public ReserveService(TravelAgencyContext context)
    {
        Context = context;
    }

    public async Task<ApiResponse<IdResponse>> CreateReserve(ReserveRequest<T1, T2> request, UserBasic userBasic)
    {
        var package = await Context.Packages
            .Include(x => x.FlightOffers)
            .ThenInclude(o => o.Reserves)
            .Include(x => x.HotelOffers)
            .ThenInclude(o => o.Reserves)
            .Include(x => x.ExcursionOffers)
            .ThenInclude(o => o.Reserves)
            .FirstOrDefaultAsync(request.IsSingleOffer
                ? p => p.IsSingleOffer &&
                       (p.HotelOffers.Any(o => o.Id == request.Id) ||
                        p.ExcursionOffers.Any(o => o.Id == request.Id) ||
                        p.FlightOffers.Any(o => o.Id == request.Id))
                : p => p.Id == request.Id);
        if (package is null)
            return new NotFound<IdResponse>("Package not found");

        if (!await CheckPermissions(userBasic, package))
            return new Unauthorized<IdResponse>("You don't have permissions");


        if (!CheckAvailability(package, request.UserIdentities.Count))
            return new BadRequest<IdResponse>("There is no availability for this package");

        if (!CheckUniqueIdentity(request.UserIdentities))
            return new BadRequest<IdResponse>("There are repeated identities");

        if (!Helpers.ValidDate(package.StartDate()))
            return new BadRequest<IdResponse>($"The {(package.IsSingleOffer ? "offer" : "package")} has expired");

        var price = package.Price();

        var identity = await Helpers.ManageUserIdentity(new[] { request.UserIdentity }, Context);
        var payment = request.Payment(identity[0].Id, price);
        Context.Set<T2>().Add(payment);
        await Context.SaveChangesAsync();

        var reserve = request.Reserve(package.Id, payment.Id, userBasic.Id, request.UserIdentities.Count);
        AddOffers(package, reserve);

        var identities = await Helpers.ManageUserIdentity(request.UserIdentities, Context);
        reserve.UserIdentities = identities;

        Context.Set<T1>().Add(reserve);
        await Context.SaveChangesAsync();

        return new ApiResponse<IdResponse>(new IdResponse { Id = reserve.Id });
    }

    private bool CheckUniqueIdentity(ICollection<UserIdentityRequest> identityRequests) =>
        identityRequests.Select(x => new { x.Name, x.Nationality, x.IdentityDocument }).Distinct().Count() ==
        identityRequests.Count;

    private bool CheckAvailability(Package package, int cant)
    {
        foreach (var offer in package.HotelOffers)
        {
            var count = offer.Reserves.Sum(x => x.Cant);
            if (count + cant > offer.Availability) return false;
        }

        foreach (var offer in package.ExcursionOffers)
        {
            var count = offer.Reserves.Sum(x => x.Cant);
            if (count + cant > offer.Availability) return false;
        }

        foreach (var offer in package.FlightOffers)
        {
            var count = offer.Reserves.Sum(x => x.Cant);
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

    internal abstract Task<bool> CheckPermissions(UserBasic user, Package package);
}