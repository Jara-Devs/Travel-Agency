using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;
using Travel_Agency_Core;

namespace Travel_Agency_Logic.Core;

public interface IAuthenticationService
{
    Task<ApiResponse<LoginResponse>> Login(LoginRequest login);

    Task<ApiResponse<LoginResponse>> RegisterTourist(RegisterTouristRequest touristRequest);

    Task<ApiResponse<LoginResponse>> RegisterAgency(RegisterAgencyRequest agencyRequest);

    Task<ApiResponse<IdResponse>> RegisterManagerAgency(RegisterUserAgencyRequest userAgencyRequest, UserBasic user);

    Task<ApiResponse<IdResponse>> RegisterEmployeeAgency(RegisterUserAgencyRequest userAgencyRequest, UserBasic user);

    Task<ApiResponse> ChangePassword(ChangePasswordRequest request, UserBasic user);

    ApiResponse<LoginResponse> Renew(UserBasic user);
}