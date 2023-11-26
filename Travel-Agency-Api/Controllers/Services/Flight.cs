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
public class FlightController : TravelAgencyController
{
    private readonly IFlightService _flightService;

    private readonly IQueryEntity<Flight> _query;

    public FlightController(IFlightService flightService, IQueryEntity<Flight> query)
    {
        this._flightService = flightService;
        this._query = query;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get(ODataQueryOptions<Flight> options)
    {
        var user = GetUser().Value!;
        var response = await this._query.Get(user);

        return OdataResponse(response, options);
    }

    [HttpGet("{key}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromODataUri] Guid key, ODataQueryOptions<Flight> options)
    {
        var user = GetUser().Value!;
        var response = await this._query.Get(user);

        return OdataSingleResponse(response, options, x => x.Id == key);
    }


    [HttpPost]
    public async Task<IActionResult> CreateFlight([FromBody] FlightRequest flight)
    {
        var user = GetUser().Value;
        return ToResponse(await _flightService.CreateFlight(flight, user!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFlight(Guid id, [FromBody] FlightRequest flight)
    {
        var user = GetUser().Value;
        return ToResponse(await _flightService.UpdateFlight(id, flight, user!));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFlight(Guid id)
    {
        var user = GetUser().Value;
        return ToResponse(await _flightService.DeleteFlight(id, user!));
    }
}