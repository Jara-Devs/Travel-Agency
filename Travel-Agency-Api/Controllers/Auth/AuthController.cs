using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_Agency_Api.Core;
using Travel_Agency_Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Core;

namespace Travel_Agency_Api.Controllers.Auth;

[ApiController]
[Route("[controller]")]
public class AuthController : TravelAgencyController
{
    private readonly IAuthenticationService _authService;

    public AuthController(IAuthenticationService authService)
    {
        this._authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request) => ToResponse(await this._authService.Login(request));
    
    [HttpPost("register/tourist")]
    public async Task<IActionResult> RegisterTourist([FromBody] RegisterTouristRequest request) =>
        ToResponse(await this._authService.RegisterTourist(request));

    [HttpPost("register/agency")]
    public async Task<IActionResult> RegisterAgency([FromBody] RegisterAgencyRequest request) =>
        ToResponse(await this._authService.RegisterAgency(request));

    [HttpPost("changePassword")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var user = GetUser().Value!;
        return ToResponse(await this._authService.ChangePassword(request, user));
    }

    [HttpPost("renew")]
    [Authorize]
    public IActionResult Renew()
    {
        var user = GetUser().Value!;
        return ToResponse(this._authService.Renew(user));
    }
}