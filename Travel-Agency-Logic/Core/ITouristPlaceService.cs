using Travel_Agency_Core;
using Travel_Agency_Domain.Services;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Core;

public interface ITouristPlaceService
{
    Task<ApiResponse<IdResponse>> CreateTouristPlace(TouristPlaceRequest touristPlace, UserBasic user);
    Task<ApiResponse> UpdateTouristPlace(Guid id, TouristPlaceRequest touristPlace, UserBasic user);
    Task<ApiResponse> DeleteTouristPlace(Guid id, UserBasic user);
}