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
public class TouristActivityController : TravelAgencyController
{
    private readonly IQueryEntity<TouristActivity> _query;
    private readonly ITouristActivityService _touristActivityService;

    public TouristActivityController(ITouristActivityService touristActivityService,
        IQueryEntity<TouristActivity> query)
    {
        _touristActivityService = touristActivityService;
        _query = query;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get(ODataQueryOptions<TouristActivity> options)
    {
        var user = GetUser().Value!;
        var response = await _query.Get(user);

        return OdataResponse(response, options);
    }

    [HttpGet("{key}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromODataUri] Guid key, ODataQueryOptions<TouristActivity> options)
    {
        var user = GetUser().Value!;
        var response = await _query.Get(user);

        return OdataSingleResponse(response, options, x => x.Id == key);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTouristActivity([FromBody] TouristActivityRequest touristActivity)
    {
        var user = GetUser().Value;
        return ToResponse(await _touristActivityService.CreateTouristActivity(touristActivity, user!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTouristActivity(Guid id, [FromBody] TouristActivityRequest touristActivity)
    {
        var user = GetUser().Value;
        return ToResponse(await _touristActivityService.UpdateTouristActivity(id, touristActivity, user!));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTouristActivity(Guid id)
    {
        var user = GetUser().Value;
        return ToResponse(await _touristActivityService.DeleteTouristActivity(id, user!));
    }
}