using System.Linq.Expressions;
using Travel_Agency_DataBase.Core;

namespace Travel_Agency_DataBase;

public class Query<TEntity> : IQuery<TEntity> where TEntity : class
{
    private readonly TravelAgencyContext _context;

    private Expression<Func<TEntity, bool>>? _filter;

    public Query(TravelAgencyContext context)
    {
        this._context = context;
    }

    public void Filter(Expression<Func<TEntity, bool>> filter)
    {
        this._filter = filter;
    }

    public IQueryable<TEntity> Get() => this._filter is null
        ? this._context.Set<TEntity>()
        : this._context.Set<TEntity>().Where(this._filter);

    public TEntity? SingleGet() => Get().SingleOrDefault();
}