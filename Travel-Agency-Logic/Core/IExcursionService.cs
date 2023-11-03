using Travel_Agency_Core;
using Travel_Agency_Domain.Services;
using Travel_Agency_Logic.Request;

namespace Travel_Agency_Logic.Core;

public interface IExcursionService
{
    Task<ApiResponse<Excursion>> CreateExcursion(ExcursionRequest excursion, UserBasic user);
    Task<ApiResponse<Excursion>> UpdateExcursion(int id, ExcursionRequest excursion, UserBasic user);
    Task<ApiResponse<Excursion>> DeleteExcursion(int id, UserBasic user);
}