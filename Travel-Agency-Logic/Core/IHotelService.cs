using Travel_Agency_Core;
using Travel_Agency_Domain.Services;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Core;

public interface IHotelService
{
    Task<ApiResponse<IdResponse>> CreateHotel(HotelRequest hotel, UserBasic user);
    Task<ApiResponse> UpdateHotel(int id, HotelRequest hotel, UserBasic user);
    Task<ApiResponse> DeleteHotel(int id, UserBasic user);
}