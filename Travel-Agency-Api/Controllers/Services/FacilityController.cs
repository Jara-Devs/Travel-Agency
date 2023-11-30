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

namespace Travel_Agency_Api.Controllers.Services;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = Policies.App)]
public class FacilityController : TravelAgencyController
{
    private readonly IFacilityService _facilityService;

    private readonly IQueryEntity<Facility> _query;

    public FacilityController(IFacilityService facilityService, IQueryEntity<Facility> query)
    {
        _facilityService = facilityService;
        _query = query;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get(ODataQueryOptions<Facility> options)
    {
        var user = GetUser().Value!;
        var response = await _query.Get(user);

        return OdataResponse(response, options);
    }

    [HttpGet("{key}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromODataUri] Guid key, ODataQueryOptions<Facility> options)
    {
        var user = GetUser().Value!;
        var response = await _query.Get(user);

        return OdataSingleResponse(response, options, x => x.Id == key);
    }


    [HttpPost]
    public async Task<IActionResult> CreateFacility([FromBody] FacilityRequest facility)
    {
        var user = GetUser().Value;
        return ToResponse(await _facilityService.CreateFacility(facility, user!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFacility(Guid id, [FromBody] FacilityRequest facility)
    {
        var user = GetUser().Value;
        return ToResponse(await _facilityService.UpdateFacility(id, facility, user!));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFacility(Guid id)
    {
        var user = GetUser().Value;
        return ToResponse(await _facilityService.DeleteFacility(id, user!));
    }
}