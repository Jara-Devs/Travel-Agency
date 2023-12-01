using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Payments;

namespace Travel_Agency_Logic.Reserves;

public class ReserveTicketService : ReserveService<ReserveTicket, PaymentTicket>
{
    public ReserveTicketService(TravelAgencyContext context) : base(context)
    {
    }

    internal override bool CheckPermissions(UserBasic user)
    {
        return user.Role == Roles.EmployeeAgency || user.Role == Roles.ManagerAgency || user.Role == Roles.AdminAgency;
    }
}