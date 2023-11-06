using Travel_Agency_Core;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_DataBase.Queries.Offers;

public class OfferQuery<T> : IQueryEntity<T> where T : Offer
{
    private readonly TravelAgencyContext _context;

    public OfferQuery(TravelAgencyContext context)
    {
        this._context = context;
    }

    public async Task<ApiResponse<IQueryable<T>>> Get(UserBasic userBasic)
    {
        if (userBasic.Role != Roles.ManagerAgency && userBasic.Role != Roles.AdminAgency)
            return new Unauthorized<IQueryable<T>>("You not are manager or admin of this agency");

        var user = await this._context.UserAgencies.FindAsync(userBasic.Id);

        return new ApiResponse<IQueryable<T>>(this._context.Set<T>().Where(x => x.AgencyId == user!.AgencyId));
    }
}