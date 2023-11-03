using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_Agency_Api.Core;
using Travel_Agency_Core;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;

namespace Travel_Agency_Api.Controllers.Services;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = Policies.App)]
public class TouristPlaceController : TravelAgencyController
{
    private readonly ITouristPlaceService _touristPlaceService;

    public TouristPlaceController(ITouristPlaceService touristPlaceService)
    {
        this._touristPlaceService = touristPlaceService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTouristPlace([FromBody] TouristicPlaceRequest touristPlace)
    {
        var user = GetUser().Value;
        return ToResponse(await _touristPlaceService.CreateTouristPlace(touristPlace, user!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTouristPlace(int id, [FromBody] TouristicPlaceRequest touristPlace)
    {
        var user = GetUser().Value;
        return ToResponse(await _touristPlaceService.UpdateTouristPlace(id, touristPlace, user!));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTouristPlace(int id)
    {
        var user = GetUser().Value;
        return ToResponse(await _touristPlaceService.DeleteTouristPlace(id, user!));
    }
}