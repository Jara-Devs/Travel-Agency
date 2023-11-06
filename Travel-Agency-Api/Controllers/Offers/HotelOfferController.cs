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
public class HotelOfferController : TravelAgencyController
{
    private readonly IOfferService<HotelOffer> _hotelOfferService;

    public HotelOfferController(IOfferService<HotelOffer> hotelOfferService)
    {
        _hotelOfferService = hotelOfferService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateHotelOffer([FromBody] HotelOfferRequest hotelOffer)
    {
        var user = GetUser().Value;
        return ToResponse(await _hotelOfferService.CreateOffer(hotelOffer, user!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHotelOffer(int id, [FromBody] HotelOfferRequest hotelOffer)
    {
        var user = GetUser().Value;
        return ToResponse(await _hotelOfferService.UpdateOffer(id, hotelOffer, user!));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotelOffer(int id)
    {
        var user = GetUser().Value;
        return ToResponse(await _hotelOfferService.DeleteOffer(id, user!));
    }
}