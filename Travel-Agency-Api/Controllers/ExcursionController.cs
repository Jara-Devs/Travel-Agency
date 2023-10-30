using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_Agency_Api.Core;
using Travel_Agency_Domain.Services;
using Travel_Agency_Logic.Core;

namespace Travel_Agency_Api.Controllers;

[ApiController]
[Route("[controller]")]
// [Authorize(Roles="AdminApp,EmployeeApp")]
public class ExcusionController : TravelAgencyController
{
    private readonly IExcursionService _excursionService;

    public ExcusionController(IExcursionService excursionService)
    {
        this._excursionService = excursionService;
    }

    [HttpPost]
    public async Task<IActionResult> Createexcursion([FromBody] Excursion excursion)
    {
        var user = GetUser().Value;
        return ToResponse(await _excursionService.CreateExcursion(excursion, user!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Updateexcursion(int id, [FromBody] Excursion excursion)
    {
        var user = GetUser().Value;
        return ToResponse(await _excursionService.UpdateExcursion(excursion, user!));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deleteexcursion(int id)
    {
        var user = GetUser().Value;
        return ToResponse(await _excursionService.DeleteExcursion(id, user!));
    }
}