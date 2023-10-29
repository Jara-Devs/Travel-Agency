using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_Agency_Api.Core;
using Travel_Agency_Domain.Services;
using Travel_Agency_Logic.Core;

namespace Travel_Agency_Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles="AdminApp,EmployeeApp")]
public class TouristPlaceController : TravelAgencyController
{
    private readonly ITouristPlaceService _touristPlaceService;

    public TouristPlaceController(ITouristPlaceService touristPlaceService)
    {
        this._touristPlaceService = touristPlaceService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatetouristPlace([FromBody] TouristPlace touristPlace)
    {
        var user = GetUser().Value;
        return ToResponse(await _touristPlaceService.CreateTouristPlace(touristPlace, user!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatetouristPlace(int id, [FromBody] TouristPlace touristPlace)
    {
        var user = GetUser().Value;
        return ToResponse(await _touristPlaceService.UpdateTouristPlace(touristPlace, user!));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletetouristPlace(int id)
    {
        var user = GetUser().Value;
        return ToResponse(await _touristPlaceService.DeleteTouristPlace(id, user!));
    }
}