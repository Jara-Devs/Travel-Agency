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
public class ExcursionController : TravelAgencyController
{
    private readonly IExcursionService _excursionService;

    public ExcursionController(IExcursionService excursionService)
    {
        this._excursionService = excursionService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateExcursion([FromBody] ExcursionRequest excursion)
    {
        var user = GetUser().Value;
        return ToResponse(await _excursionService.CreateExcursion(excursion, user!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExcursion(int id, [FromBody] ExcursionRequest excursion)
    {
        var user = GetUser().Value;
        return ToResponse(await _excursionService.UpdateExcursion(id, excursion, user!));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExcursion(int id)
    {
        var user = GetUser().Value;
        return ToResponse(await _excursionService.DeleteExcursion(id, user!));
    }
}