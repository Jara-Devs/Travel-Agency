using Travel_Agency_Core;
using Travel_Agency_Domain.Services;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Core;

public interface IExcursionService
{
    Task<ApiResponse<IdResponse>> CreateExcursion(ExcursionRequest excursion, UserBasic user);
    Task<ApiResponse> UpdateExcursion(int id, ExcursionRequest excursion, UserBasic user);
    Task<ApiResponse> DeleteExcursion(int id, UserBasic user);
}