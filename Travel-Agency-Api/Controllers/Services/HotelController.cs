using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_Agency_Api.Core;
using Travel_Agency_Core;
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

    public HotelController(IHotelService hotelService)
    {
        this._hotelService = hotelService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateHotel([FromBody] HotelRequest hotel)
    {
        var user = GetUser().Value;
        return ToResponse(await _hotelService.CreateHotel(hotel, user!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHotel(int id, [FromBody] HotelRequest hotel)
    {
        var user = GetUser().Value;
        return ToResponse(await _hotelService.UpdateHotel(id, hotel, user!));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        var user = GetUser().Value;
        return ToResponse(await _hotelService.DeleteHotel(id, user!));
    }
}