using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Travel_Agency_Api.Core;
using Travel_Agency_Core;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Services;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;

namespace Travel_Agency_Api.Controllers.Services;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = Policies.App)]
public class ExcursionController : TravelAgencyController
{
    private readonly IExcursionService _excursionService;

    private readonly IQueryEntity<Excursion> _query;

    public ExcursionController(IExcursionService excursionService, IQueryEntity<Excursion> query)
    {
        _excursionService = excursionService;
        _query = query;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get(ODataQueryOptions<Excursion> options)
    {
        var user = GetUser().Value!;
        var response = await _query.Get(user);

        return OdataResponse(response, options);
    }

    [HttpGet("{key}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromODataUri] Guid key, ODataQueryOptions<Excursion> options)
    {
        var user = GetUser().Value!;
        var response = await _query.Get(user);

        return OdataSingleResponse(response, options, x => x.Id == key);
    }


    [HttpPost]
    public async Task<IActionResult> CreateExcursion([FromBody] ExcursionRequest excursion)
    {
        var user = GetUser().Value;
        return ToResponse(await _excursionService.CreateExcursion(excursion, user!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExcursion(Guid id, [FromBody] ExcursionRequest excursion)
    {
        var user = GetUser().Value;
        return ToResponse(await _excursionService.UpdateExcursion(id, excursion, user!));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExcursion(Guid id)
    {
        var user = GetUser().Value;
        return ToResponse(await _excursionService.DeleteExcursion(id, user!));
    }
}