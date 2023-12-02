using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Packages;
using Travel_Agency_Domain.Payments;

namespace Travel_Agency_Logic.Reserves;

public class ReserveTicketService : ReserveService<ReserveTicket, PaymentTicket>
{
    public ReserveTicketService(TravelAgencyContext context) : base(context)
    {
    }

    internal override async Task<bool> CheckPermissions(UserBasic user, Package package)
    {
        if (!(user.Role == Roles.EmployeeAgency || user.Role == Roles.ManagerAgency || user.Role == Roles.AdminAgency))
            return false;

        var agency = await Context.UserAgencies.FindAsync(user.Id);
        if (agency is null) return false;

        return Packages.PackageService.PackageAgencyId(package) == agency.AgencyId;
    }
}