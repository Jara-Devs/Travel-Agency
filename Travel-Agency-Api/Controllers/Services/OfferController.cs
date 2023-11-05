using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_Agency_Api.Core;
using Travel_Agency_Core;
using Travel_Agency_Domain.Offers;
using Travel_Agency_Domain.Services;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;

namespace Travel_Agency_Api.Controllers.Services;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = Policies.AgencyManager)]
public class OfferController : TravelAgencyController
{
    private readonly IOfferService<HotelOffer> _hotelOfferService;
    private readonly IOfferService<ExcursionOffer> _excursionOfferService;
    private readonly IOfferService<FlightOffer> _flightOfferService;

    public OfferController(IOfferService<HotelOffer> hotelOfferService, 
        IOfferService<ExcursionOffer> excursionOfferService, 
        IOfferService<FlightOffer> flightOfferService) {
        _hotelOfferService = hotelOfferService;
        _excursionOfferService = excursionOfferService;
        _flightOfferService = flightOfferService;
    }

    [HttpPost("hotel")]
    public async Task<IActionResult> CreateHotelOffer([FromBody] HotelOfferRequest hotelOffer) {
        var user = GetUser().Value;
        return ToResponse(await _hotelOfferService.CreateOffer(hotelOffer, user!));
    }

    [HttpPut("hotel/{id}")]
    public async Task<IActionResult> UpdateHotelOffer(int id, [FromBody] HotelOfferRequest hotelOffer) {
        var user = GetUser().Value;
        return ToResponse(await _hotelOfferService.UpdateOffer(id, hotelOffer, user!));
    }

    [HttpDelete("hotel/{id}")]
    public async Task<IActionResult> DeleteHotelOffer(int id) {
        var user = GetUser().Value;
        return ToResponse(await _hotelOfferService.DeleteOffer(id, user!));
    }

    [HttpPost("excursion")]
    public async Task<IActionResult> CreateExcursionOffer([FromBody] ExcursionOfferRequest excursionOffer) {
        var user = GetUser().Value;
        return ToResponse(await _excursionOfferService.CreateOffer(excursionOffer, user!));
    }

    [HttpPut("excursion/{id}")]
    public async Task<IActionResult> UpdateExcursionOffer(int id, [FromBody] ExcursionOfferRequest excursionOffer) {
        var user = GetUser().Value;
        return ToResponse(await _excursionOfferService.UpdateOffer(id, excursionOffer, user!));
    }

    [HttpDelete("excursion/{id}")]
    public async Task<IActionResult> DeleteExcursionOffer(int id) {
        var user = GetUser().Value;
        return ToResponse(await _excursionOfferService.DeleteOffer(id, user!));
    }

    [HttpPost("flight")]
    public async Task<IActionResult> CreateFlightOffer([FromBody] FlightOfferRequest flightOffer) {
        var user = GetUser().Value;
        return ToResponse(await _flightOfferService.CreateOffer(flightOffer, user!));
    }

    [HttpPut("flight/{id}")]
    public async Task<IActionResult> UpdateFlightOffer(int id, [FromBody] FlightOfferRequest flightOffer) {
        var user = GetUser().Value;
        return ToResponse(await _flightOfferService.UpdateOffer(id, flightOffer, user!));
    }

    [HttpDelete("flight/{id}")]
    public async Task<IActionResult> DeleteFlightOffer(int id) {
        var user = GetUser().Value;
        return ToResponse(await _flightOfferService.DeleteOffer(id, user!));
    }
}