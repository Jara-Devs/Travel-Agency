using Travel_Agency_Core;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Payments;

namespace Travel_Agency_DataBase.Queries.Users;

public class ReserveQuery<T> : IQueryEntity<T> where T : Reserve
{
    private readonly TravelAgencyContext _context;

    public ReserveQuery(TravelAgencyContext context)
    {
        this._context = context;
    }

    public Task<ApiResponse<IQueryable<T>>> Get(UserBasic userBasic)
    {
        if (userBasic.Role == Roles.Tourist || userBasic.Role == Roles.EmployeeAgency)
            return Task.FromResult(new ApiResponse<IQueryable<T>>(
                this._context.Set<T>().Where(x => x.UserId == userBasic.Id)));
        else if (userBasic.Role == Roles.AdminAgency) 
            return Task.FromResult(new ApiResponse<IQueryable<T>>(
                this._context.Set<T>().Where(x => x.Package.HotelOffers.First().AgencyId == userBasic.Id)));
        else if (userBasic.Role == Roles.AdminApp)
            return Task.FromResult(new ApiResponse<IQueryable<T>>(this._context.Set<T>()));
        else
            return Task.FromResult(new Unauthorized<IQueryable<T>>("You are not allowed to see these reserves")
                as ApiResponse<IQueryable<T>>);
    }
}