using Travel_Agency_Core.Request;
using Travel_Agency_Core.Response;
using Travel_Agency_Core;

namespace Travel_Agency_Logic.Core;

public interface IAuthenticationService
{
    Task<ApiResponse<LoginResponse>> Login(LoginRequest login);

    Task<ApiResponse<LoginResponse>> RegisterTourist(RegisterTouristRequest touristRequest);

    Task<ApiResponse<LoginResponse>> RegisterAgency(RegisterAgencyRequest agencyRequest);

    ApiResponse<LoginResponse> Renew(UserBasic user);
}