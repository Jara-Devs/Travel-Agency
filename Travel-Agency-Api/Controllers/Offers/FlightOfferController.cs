using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_Agency_Api.Core;
using Travel_Agency_Core;
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

    public FlightOfferController(IOfferService<FlightOffer> flightOfferService)
    {
        _flightOfferService = flightOfferService;
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