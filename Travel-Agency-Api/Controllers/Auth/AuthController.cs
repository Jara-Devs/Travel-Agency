using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_Agency_Api.Core;
using Travel_Agency_Core;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Api.Controllers.Auth;

[ApiController]
[Route("[controller]")]
public class AuthController : TravelAgencyController
{
    private readonly IAuthenticationService _authService;

    public AuthController(IAuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        return LoginResponse(await _authService.Login(request));
    }

    [HttpPost("register/tourist")]
    public async Task<IActionResult> RegisterTourist([FromBody] RegisterTouristRequest request)
    {
        return LoginResponse(await _authService.RegisterTourist(request));
    }

    [HttpPost("register/agency")]
    public async Task<IActionResult> RegisterAgency([FromBody] RegisterAgencyRequest request)
    {
        return LoginResponse(await _authService.RegisterAgency(request));
    }

    [HttpPost("changePassword")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var user = GetUser().Value!;
        return ToResponse(await _authService.ChangePassword(request, user));
    }

    [HttpPost("renew")]
    [Authorize]
    public async Task<IActionResult> Renew()
    {
        var user = GetUser().Value!;
        return LoginResponse(await _authService.Renew(user));
    }

    private IActionResult LoginResponse(ApiResponse<LoginResponse> response)
    {
        if (!response.Ok) return ToResponse(response);

        if (response.Value!.Role == Roles.AdminAgency || response.Value.Role == Roles.ManagerAgency ||
            response.Value.Role == Roles.EmployeeAgency)
            return ToResponse(new ApiResponse<LoginResponseAgency>((response.Value as LoginResponseAgency)!));
        if (response.Value!.Role == Roles.Tourist)
            return ToResponse(new ApiResponse<LoginResponseTourist>((response.Value as LoginResponseTourist)!));
        return ToResponse(response);
    }
}