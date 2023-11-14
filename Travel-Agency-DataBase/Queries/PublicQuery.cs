using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase.Core;

namespace Travel_Agency_DataBase.Queries;

public class PublicQuery<T>:IQueryEntity<T> where T:Entity
{
    private readonly TravelAgencyContext _context;

    public PublicQuery(TravelAgencyContext context)
    {
        this._context = context;
    }

    public Task<ApiResponse<IQueryable<T>>> Get(UserBasic _) =>
        Task.FromResult(new ApiResponse<IQueryable<T>>(this._context.Set<T>().AsNoTracking()));
}