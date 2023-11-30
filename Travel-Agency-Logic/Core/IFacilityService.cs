using Travel_Agency_Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Core;

public interface IFacilityService
{
    Task<ApiResponse<IdResponse>> CreateFacility(FacilityRequest flight, UserBasic user);
    Task<ApiResponse> UpdateFacility(Guid id, FacilityRequest flight, UserBasic user);
    Task<ApiResponse> DeleteFacility(Guid id, UserBasic user);
}