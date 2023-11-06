using Travel_Agency_Core;

namespace Travel_Agency_DataBase.Core;

public interface IQueryEntity<TEntity> where TEntity : Entity
{
    Task<ApiResponse<IQueryable<TEntity>>> Get(UserBasic userBasic);
}