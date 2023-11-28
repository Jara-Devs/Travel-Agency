using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Travel_Agency_Api.Core;
using Travel_Agency_Core;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Users;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;

namespace Travel_Agency_Api.Controllers.Auth;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = Roles.AdminAgency)]
public class UserAgencyController : TravelAgencyController
{
    private readonly IAuthenticationService _authService;

    private readonly IQueryEntity<UserAgency> _query;

    public UserAgencyController(IAuthenticationService authService, IQueryEntity<UserAgency> query)
    {
        _authService = authService;
        _query = query;
    }

    [HttpGet]
    public async Task<IActionResult> Get(ODataQueryOptions<UserAgency> options)
    {
        var user = GetUser().Value!;
        var response = await _query.Get(user);

        return OdataResponse(response, options);
    }

    [HttpGet("{key}")]
    public async Task<IActionResult> Get([FromODataUri] Guid key, ODataQueryOptions<UserAgency> options)
    {
        var user = GetUser().Value!;
        var response = await _query.Get(user);

        return OdataSingleResponse(response, options, x => x.Id == key);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserAgencyRequest request)
    {
        var user = GetUser().Value!;
        return ToResponse(await _authService.RegisterUserAgency(request, user));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var user = GetUser().Value!;
        return ToResponse(await _authService.RemoveUserAgency(id, user));
    }
}