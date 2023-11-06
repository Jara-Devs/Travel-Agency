using Travel_Agency_Core;
using Travel_Agency_DataBase.Core;

namespace Travel_Agency_DataBase.Queries.Services;

public class ServiceQuery<T> : IQueryEntity<T> where T : Entity
{
    private readonly TravelAgencyContext _context;

    public ServiceQuery(TravelAgencyContext context)
    {
        this._context = context;
    }

    public Task<ApiResponse<IQueryable<T>>> Get(UserBasic userBasic)
    {
        if (userBasic.Role != Roles.EmployeeApp && userBasic.Role != Roles.AdminApp)
            return Task.FromResult(
                new Unauthorized<IQueryable<T>>("You not are employ or admin app") as ApiResponse<IQueryable<T>>);

        return Task.FromResult(new ApiResponse<IQueryable<T>>(this._context.Set<T>()));
    }
}