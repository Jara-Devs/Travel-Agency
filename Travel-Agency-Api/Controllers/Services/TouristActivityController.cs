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
public class TouristActivityController : TravelAgencyController
{
    private readonly ITouristActivityService _touristActivityService;

    public TouristActivityController(ITouristActivityService touristActivityService)
    {
        this._touristActivityService = touristActivityService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTouristActivity([FromBody] TouristActivityRequest touristActivity)
    {
        var user = GetUser().Value;
        return ToResponse(await _touristActivityService.CreateTouristActivity(touristActivity, user!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTouristActivity(int id, [FromBody] TouristActivityRequest touristActivity)
    {
        var user = GetUser().Value;
        return ToResponse(await _touristActivityService.UpdateTouristActivity(id, touristActivity, user!));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTouristActivity(int id)
    {
        var user = GetUser().Value;
        return ToResponse(await _touristActivityService.DeleteTouristActivity(id, user!));
    }
}