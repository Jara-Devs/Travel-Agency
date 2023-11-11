using Travel_Agency_Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Core;

public interface IOverNightExcursionService
{
    Task<ApiResponse<IdResponse>> CreateOverNightExcursion(OverNightExcursionRequest excursion, UserBasic user);
    Task<ApiResponse> UpdateOverNightExcursion(int id, OverNightExcursionRequest excursion, UserBasic user);
    Task<ApiResponse> DeleteOverNightExcursion(int id, UserBasic user);
}