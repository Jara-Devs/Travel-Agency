using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Travel_Agency_Api.Core;
using Travel_Agency_Core;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Offers;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;

namespace Travel_Agency_Api.Controllers.Offers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = Policies.AgencyManager)]
public class FlightOfferController : TravelAgencyController
{
    private readonly IOfferService<FlightOffer> _flightOfferService;

    private readonly IQueryEntity<FlightOffer> _query;

    public FlightOfferController(IOfferService<FlightOffer> flightOfferService, IQueryEntity<FlightOffer> query)
    {
        _flightOfferService = flightOfferService;
        _query = query;
    }

    [HttpGet]
    public async Task<IActionResult> Get(ODataQueryOptions<FlightOffer> options)
    {
        var user = GetUser().Value!;
        var response = await _query.Get(user);

        return OdataResponse(response, options);
    }

    [HttpGet("{key}")]
    public async Task<IActionResult> Get([FromODataUri] int key, ODataQueryOptions<FlightOffer> options)
    {
        var user = GetUser().Value!;
        var response = await _query.Get(user);

        return OdataSingleResponse(response, options, x => x.Id == key);
    }


    [HttpPost]
    public async Task<IActionResult> CreateFlightOffer([FromBody] FlightOfferRequest flightOffer)
    {
        var user = GetUser().Value;
        return ToResponse(await _flightOfferService.CreateOffer(flightOffer, user!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFlightOffer(int id, [FromBody] FlightOfferRequest flightOffer)
    {
        var user = GetUser().Value;
        return ToResponse(await _flightOfferService.UpdateOffer(id, flightOffer, user!));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFlightOffer(int id)
    {
        var user = GetUser().Value;
        return ToResponse(await _flightOfferService.DeleteOffer(id, user!));
    }
}