using Travel_Agency_Core;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Users;

namespace Travel_Agency_DataBase.Queries.Users;

public class UserAgencyQuery : IQueryEntity<UserAgency>
{
    private readonly TravelAgencyContext _context;

    public UserAgencyQuery(TravelAgencyContext context)
    {
        this._context = context;
    }

    public async Task<ApiResponse<IQueryable<UserAgency>>> Get(UserBasic userBasic)
    {
        if (userBasic.Role != Roles.AdminAgency)
            return new Unauthorized<IQueryable<UserAgency>>("You are not an admin of this agency");

        var admin = await this._context.UserAgencies.FindAsync(userBasic.Id);

        return new ApiResponse<IQueryable<UserAgency>>(
            this._context.UserAgencies.Where(x =>x.AgencyId == admin!.AgencyId));
    }
}