using Travel_Agency_Core;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Core;

public interface IHotelService
{
    Task<ApiResponse<Hotel>> CreateHotel(Hotel hotel, UserBasic user);
    Task<ApiResponse<Hotel>> UpdateHotel(Hotel hotel, UserBasic user);
    Task<ApiResponse<Hotel>> DeleteHotel(int id, UserBasic user);
}