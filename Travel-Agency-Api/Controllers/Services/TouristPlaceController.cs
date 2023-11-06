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
public class TouristPlaceController : TravelAgencyController
{
    private readonly ITouristPlaceService _touristPlaceService;

    private readonly IQueryEntity<TouristPlace> _query;

    public TouristPlaceController(ITouristPlaceService touristPlaceService, IQueryEntity<TouristPlace> query)
    {
        this._touristPlaceService = touristPlaceService;
        this._query = query;
    }

    [HttpGet]
    public async Task<IActionResult> Get(ODataQueryOptions<TouristPlace> options)
    {
        var user = GetUser().Value!;
        var response = await this._query.Get(user);

        return OdataResponse(response, options);
    }

    [HttpGet("{key}")]
    public async Task<IActionResult> Get([FromODataUri] int key, ODataQueryOptions<TouristPlace> options)
    {
        var user = GetUser().Value!;
        var response = await this._query.Get(user);

        return OdataSingleResponse(response, options, x => x.Id == key);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTouristPlace([FromBody] TouristPlaceRequest touristPlace)
    {
        var user = GetUser().Value;
        return ToResponse(await _touristPlaceService.CreateTouristPlace(touristPlace, user!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTouristPlace(int id, [FromBody] TouristPlaceRequest touristPlace)
    {
        var user = GetUser().Value;
        return ToResponse(await _touristPlaceService.UpdateTouristPlace(id, touristPlace, user!));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTouristPlace(int id)
    {
        var user = GetUser().Value;
        return ToResponse(await _touristPlaceService.DeleteTouristPlace(id, user!));
    }
}