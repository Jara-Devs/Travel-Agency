using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Packages;
using Travel_Agency_Domain.Payments;

namespace Travel_Agency_Logic.Reserves;

public class ReserveTouristService : ReserveService<ReserveTourist, PaymentOnline>
{
    public ReserveTouristService(TravelAgencyContext context) : base(context)
    {
    }

    internal override Task<bool> CheckPermissions(UserBasic user, Package _)
    {
        return Task.FromResult(user.Role == Roles.Tourist);
    }
}