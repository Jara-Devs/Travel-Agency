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
public class HotelOfferController : TravelAgencyController
{
    private readonly IOfferService<HotelOffer> _hotelOfferService;

    private readonly IQueryEntity<HotelOffer> _query;

    public HotelOfferController(IOfferService<HotelOffer> hotelOfferService, IQueryEntity<HotelOffer> query)
    {
        _hotelOfferService = hotelOfferService;
        _query = query;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get(ODataQueryOptions<HotelOffer> options)
    {
        var user = GetUser().Value!;
        var response = await _query.Get(user);

        return OdataResponse(response, options);
    }

    [HttpGet("{key}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromODataUri] Guid key, ODataQueryOptions<HotelOffer> options)
    {
        var user = GetUser().Value!;
        var response = await _query.Get(user);

        return OdataSingleResponse(response, options, x => x.Id == key);
    }


    [HttpPost]
    public async Task<IActionResult> CreateHotelOffer([FromBody] HotelOfferRequest hotelOffer)
    {
        var user = GetUser().Value;
        return ToResponse(await _hotelOfferService.CreateOffer(hotelOffer, user!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHotelOffer(Guid id, [FromBody] HotelOfferRequest hotelOffer)
    {
        var user = GetUser().Value;
        return ToResponse(await _hotelOfferService.UpdateOffer(id, hotelOffer, user!));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotelOffer(Guid id)
    {
        var user = GetUser().Value;
        return ToResponse(await _hotelOfferService.DeleteOffer(id, user!));
    }
}