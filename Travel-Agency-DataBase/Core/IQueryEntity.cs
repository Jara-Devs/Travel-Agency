using System.Linq.Expressions;
using Travel_Agency_Core;

namespace Travel_Agency_DataBase.Core;

public interface IQueryEntity<TEntity> where TEntity : class
{
    Task<ApiResponse<IQueryable<TEntity>>> Get(UserBasic userBasic);
}