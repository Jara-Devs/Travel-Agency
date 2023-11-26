using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;
using Travel_Agency_Core;

namespace Travel_Agency_Logic.Core;

public interface IAuthenticationService
{
    Task<ApiResponse<LoginResponse>> Login(LoginRequest login);

    Task<ApiResponse<LoginResponse>> RegisterTourist(RegisterTouristRequest touristRequest);

    Task<ApiResponse<LoginResponse>> RegisterAgency(RegisterAgencyRequest agencyRequest);

    Task<ApiResponse<IdResponse>> RegisterUserAgency(RegisterUserAgencyRequest userAgencyRequest,
        UserBasic user);

    Task<ApiResponse> RemoveUserAgency(Guid id, UserBasic userBasic);

    Task<ApiResponse<IdResponse>> RegisterUserApp(RegisterUserAppRequest userAppRequest,
        UserBasic user);

    Task<ApiResponse> RemoveUserApp(Guid id, UserBasic userBasic);

    Task<ApiResponse> ChangePassword(ChangePasswordRequest request, UserBasic user);

    Task<ApiResponse<LoginResponse>> Renew(UserBasic user);
}