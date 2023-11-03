using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_Agency_Api.Core;
using Travel_Agency_Domain.Services;
using Travel_Agency_Logic.Core;

namespace Travel_Agency_Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles="AdminApp,EmployeeApp")]
public class HotelController : TravelAgencyController
{
    private readonly IHotelService _hotelService;

    public HotelController(IHotelService hotelService)
    {
        this._hotelService = hotelService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateHotel([FromBody] Hotel hotel)
    {
        var user = GetUser().Value;
        return ToResponse(await _hotelService.CreateHotel(hotel, user!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHotel(int id, [FromBody] Hotel hotel)
    {
        var user = GetUser().Value;
        return ToResponse(await _hotelService.UpdateHotel(hotel, user!));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        var user = GetUser().Value;
        return ToResponse(await _hotelService.DeleteHotel(id, user!));
    }
}