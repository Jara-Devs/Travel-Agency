using Travel_Agency_Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Core;

public interface IHotelService
{
    Task<ApiResponse<IdResponse>> CreateHotel(HotelRequest hotel, UserBasic user);
    Task<ApiResponse> UpdateHotel(Guid id, HotelRequest hotel, UserBasic user);
    Task<ApiResponse> DeleteHotel(Guid id, UserBasic user);
}