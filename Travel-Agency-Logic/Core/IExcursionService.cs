using Travel_Agency_Core;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Core;

public interface IExcursionService
{
    Task<ApiResponse<Excursion>> CreateExcursion(Excursion excursion, UserBasic user);
    Task<ApiResponse<Excursion>> UpdateExcursion(Excursion excursion, UserBasic user);
    Task<ApiResponse<Excursion>> DeleteExcursion(int id, UserBasic user);
}