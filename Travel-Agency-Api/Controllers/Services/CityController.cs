using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Travel_Agency_Api.Core;
using Travel_Agency_Core;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;

namespace Travel_Agency_Api.Controllers.Services;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = Policies.App)]
public class CityController : TravelAgencyController
{
    private readonly ICityService _cityService;

    private readonly IQueryEntity<City> _query;

    public CityController(ICityService cityService, IQueryEntity<City> query)
    {
        _cityService = cityService;
        _query = query;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get(ODataQueryOptions<City> options)
    {
        var user = GetUser().Value!;
        var response = await _query.Get(user);

        return OdataResponse(response, options);
    }

    [HttpGet("{key}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromODataUri] Guid key, ODataQueryOptions<City> options)
    {
        var user = GetUser().Value!;
        var response = await _query.Get(user);

        return OdataSingleResponse(response, options, x => x.Id == key);
    }


    [HttpPost]
    public async Task<IActionResult> CreateFacility([FromBody] CityRequest city)
    {
        var user = GetUser().Value;
        return ToResponse(await _cityService.CreateCity(city, user!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFacility(Guid id, [FromBody] CityRequest city)
    {
        var user = GetUser().Value;
        return ToResponse(await _cityService.UpdateCity(id, city, user!));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFacility(Guid id)
    {
        var user = GetUser().Value;
        return ToResponse(await _cityService.DeleteCity(id, user!));
    }
}