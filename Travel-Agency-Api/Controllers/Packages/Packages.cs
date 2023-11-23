using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Travel_Agency_Api.Core;
using Travel_Agency_Core;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Packages;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;

namespace Travel_Agency_Api.Controllers.Packages;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = Policies.AgencyManager)]
public class PackageController : TravelAgencyController
{
    private readonly IPackageService _packageService;

    private readonly IQueryEntity<Package> _query;

    public PackageController(IPackageService packageService,
        IQueryEntity<Package> query)
    {
        _packageService = packageService;
        _query = query;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get(ODataQueryOptions<Package> options)
    {
        var user = GetUser().Value!;
        var response = await this._query.Get(user);

        return OdataResponse(response, options);
    }

    [HttpGet("{key}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromODataUri] int key, ODataQueryOptions<Package> options)
    {
        var user = GetUser().Value!;
        var response = await this._query.Get(user);

        return OdataSingleResponse(response, options, x => x.Id == key);
    }


    [HttpPost]
    public async Task<IActionResult> CreatePackage([FromBody]  PackageRequest request)
    {
        var user = GetUser().Value;
        return ToResponse(await _packageService.CreatePackage(request, user!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePackage(int id, [FromBody] PackageRequest request)
    {
        var user = GetUser().Value;
        return ToResponse(await _packageService.UpdatePackage(id, request, user!));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePackage(int id)
    {
        var user = GetUser().Value;
        return ToResponse(await _packageService.RemovePackage(id, user!));
    }
}