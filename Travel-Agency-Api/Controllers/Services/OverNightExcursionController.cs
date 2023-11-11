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
public class OverNightExcursionController : TravelAgencyController
{
    private readonly IOverNightExcursionService _overNightExcursionService;

    private readonly IQueryEntity<OverNightExcursion> _query;

    public OverNightExcursionController(IOverNightExcursionService overNightExcursionService, IQueryEntity<OverNightExcursion> query)
    {
        this._overNightExcursionService = overNightExcursionService;
        this._query = query;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get(ODataQueryOptions<OverNightExcursion> options)
    {
        var user = GetUser().Value!;
        var response = await this._query.Get(user);

        return OdataResponse(response, options);
    }

    [HttpGet("{key}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromODataUri] int key, ODataQueryOptions<OverNightExcursion> options)
    {
        var user = GetUser().Value!;
        var response = await this._query.Get(user);

        return OdataSingleResponse(response, options, x => x.Id == key);
    }


    [HttpPost]
    public async Task<IActionResult> CreateExcursion([FromBody] OverNightExcursionRequest overNightExcursionService)
    {
        var user = GetUser().Value;
        return ToResponse(await _overNightExcursionService.CreateOverNightExcursion(overNightExcursionService, user!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExcursion(int id, [FromBody] OverNightExcursionRequest overNightExcursionService)
    {
        var user = GetUser().Value;
        return ToResponse(await _overNightExcursionService.UpdateOverNightExcursion(id, overNightExcursionService, user!));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExcursion(int id)
    {
        var user = GetUser().Value;
        return ToResponse(await _overNightExcursionService.DeleteOverNightExcursion(id, user!));
    }
}