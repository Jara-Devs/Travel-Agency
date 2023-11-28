using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Payments;

namespace Travel_Agency_Logic.Offers;

public class ReserveTouristService : ReserveService<ReserveTourist, PaymentOnline>
{
    public ReserveTouristService(TravelAgencyContext context) : base(context)
    {
    }

    internal override bool CheckPermissions(UserBasic user)
    {
        return user.Role == Roles.Tourist;
    }
}