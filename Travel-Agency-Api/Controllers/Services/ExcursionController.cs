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
        this._excursionService = excursionService;
        this._query = query;
    }

    [HttpGet]
    public async Task<IActionResult> Get(ODataQueryOptions<Excursion> options)
    {
        var user = GetUser().Value!;
        var response = await this._query.Get(user);

        return OdataResponse(response, options);
    }

    [HttpGet("{key}")]
    public async Task<IActionResult> Get([FromODataUri] int key, ODataQueryOptions<Excursion> options)
    {
        var user = GetUser().Value!;
        var response = await this._query.Get(user);

        return OdataSingleResponse(response, options, x => x.Id == key);
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