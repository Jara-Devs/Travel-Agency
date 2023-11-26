using Travel_Agency_Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Core;

public interface IFlightService
{
    Task<ApiResponse<IdResponse>> CreateFlight(FlightRequest flight, UserBasic user);
    Task<ApiResponse> UpdateFlight(Guid id, FlightRequest flight, UserBasic user);
    Task<ApiResponse> DeleteFlight(Guid id, UserBasic user);
}