using Travel_Agency_Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Core;

public interface ITouristActivityService
{
    Task<ApiResponse<IdResponse>> CreateTouristActivity(TouristActivityRequest touristActivity, UserBasic user);
    Task<ApiResponse> UpdateTouristActivity(int id, TouristActivityRequest touristActivity, UserBasic user);
    Task<ApiResponse> DeleteTouristActivity(int id, UserBasic user);
}