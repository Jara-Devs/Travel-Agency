using System.Linq.Expressions;

namespace Travel_Agency_DataBase.Core;

public interface IQuery<TEntity> where TEntity : class
{
    IQueryable<TEntity> Get();

    void Filter(Expression<Func<TEntity, bool>> filter);

    TEntity? SingleGet();
}