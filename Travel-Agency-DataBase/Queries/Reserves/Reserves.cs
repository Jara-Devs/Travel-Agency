using Travel_Agency_Core;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Payments;

namespace Travel_Agency_DataBase.Queries.Reserves;

public class ReserveQuery<T> : IQueryEntity<T> where T : Reserve
{
    private readonly TravelAgencyContext _context;

    public ReserveQuery(TravelAgencyContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<IQueryable<T>>> Get(UserBasic userBasic)
    {
        if (userBasic.Role == Roles.Tourist)
            return new ApiResponse<IQueryable<T>>(
                _context.Set<T>().Where(x => x.UserId == userBasic.Id));

        if (userBasic.Role == Roles.ManagerAgency || userBasic.Role == Roles.AdminAgency)
        {
            var agency = await _context.UserAgencies.FindAsync(userBasic.Id);
            if (agency is null) return new NotFound<IQueryable<T>>("Not found agency");

            return new ApiResponse<IQueryable<T>>(
                _context.Set<T>().Where(x => x.Offers.Any(o => o.AgencyId == agency!.AgencyId)));
        }


        return new Unauthorized<IQueryable<T>>("You do not have permissions");
    }
}