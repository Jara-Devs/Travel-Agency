using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Users;

namespace Travel_Agency_DataBase.Queries.Users;

public class UserAppQuery : IQueryEntity<User>
{
    private readonly TravelAgencyContext _context;

    public UserAppQuery(TravelAgencyContext context)
    {
        _context = context;
    }

    public Task<ApiResponse<IQueryable<User>>> Get(UserBasic userBasic)
    {
        if (userBasic.Role != Roles.AdminApp)
            return Task.FromResult(
                new Unauthorized<IQueryable<User>>("You are not an admin app") as ApiResponse<IQueryable<User>>);

        return Task.FromResult(new ApiResponse<IQueryable<User>>(
            _context.Users.AsNoTracking().Where(x => x.Role == Roles.EmployeeApp)));
    }
}