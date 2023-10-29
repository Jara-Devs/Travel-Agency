using Travel_Agency_Core;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Core;

public interface ITouristPlaceService
{
    Task<ApiResponse<TouristPlace>> CreateTouristPlace(TouristPlace touristPlace, UserBasic user);
    Task<ApiResponse<TouristPlace>> UpdateTouristPlace(TouristPlace touristPlace, UserBasic user);
    Task<ApiResponse<TouristPlace>> DeleteTouristPlace(int id, UserBasic user);
}