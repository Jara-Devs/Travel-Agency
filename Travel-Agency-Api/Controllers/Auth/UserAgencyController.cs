using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_Agency_Api.Core;
using Travel_Agency_Core;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;

namespace Travel_Agency_Api.Controllers.Auth;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = Roles.AdminAgency)]
public class UserAgencyController : TravelAgencyController
{
    private readonly IAuthenticationService _authService;

    public UserAgencyController(IAuthenticationService authService)
    {
        this._authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserAgencyRequest request)
    {
        var user = GetUser().Value!;
        return ToResponse(await this._authService.RegisterUserAgency(request, user));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = GetUser().Value!;
        return ToResponse(await this._authService.RemoveUserAgency(id, user));
    }
}