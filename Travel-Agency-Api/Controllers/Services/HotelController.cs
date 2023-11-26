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
public class HotelController : TravelAgencyController
{
    private readonly IHotelService _hotelService;

    private readonly IQueryEntity<Hotel> _query;

    public HotelController(IHotelService hotelService, IQueryEntity<Hotel> query)
    {
        this._hotelService = hotelService;
        this._query = query;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get(ODataQueryOptions<Hotel> options)
    {
        var user = GetUser().Value!;
        var response = await this._query.Get(user);

        return OdataResponse(response, options);
    }

    [HttpGet("{key}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromODataUri] Guid key, ODataQueryOptions<Hotel> options)
    {
        var user = GetUser().Value!;
        var response = await this._query.Get(user);

        return OdataSingleResponse(response, options, x => x.Id == key);
    }

    [HttpPost]
    public async Task<IActionResult> CreateHotel([FromBody] HotelRequest hotel)
    {
        var user = GetUser().Value;
        return ToResponse(await _hotelService.CreateHotel(hotel, user!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHotel(Guid id, [FromBody] HotelRequest hotel)
    {
        var user = GetUser().Value;
        return ToResponse(await _hotelService.UpdateHotel(id, hotel, user!));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotel(Guid id)
    {
        var user = GetUser().Value;
        return ToResponse(await _hotelService.DeleteHotel(id, user!));
    }
}