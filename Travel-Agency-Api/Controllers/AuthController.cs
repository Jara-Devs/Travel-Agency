using Microsoft.AspNetCore.Mvc;
using Travel_Agency_Api.Core;
using Travel_Agency_Core.Request;
using Travel_Agency_Logic.Core;

namespace Travel_Agency_Api.Controllers;

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
    public async Task<IActionResult> RegisterTourist(RegisterTouristRequest request) =>
        ToResponse(await this._authService.RegisterTourist(request));

    [HttpPost("register/agency")]
    public async Task<IActionResult> RegisterAgency(RegisterAgencyRequest request) =>
        ToResponse(await this._authService.RegisterAgency(request));
}