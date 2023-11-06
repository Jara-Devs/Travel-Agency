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
public class ExcursionOfferController : TravelAgencyController
{
    private readonly IOfferService<ExcursionOffer> _excursionOfferService;

    private readonly IQueryEntity<ExcursionOffer> _query;

    public ExcursionOfferController(IOfferService<ExcursionOffer> excursionOfferService,
        IQueryEntity<ExcursionOffer> query)
    {
        _excursionOfferService = excursionOfferService;
        _query = query;
    }

    [HttpGet]
    public async Task<IActionResult> Get(ODataQueryOptions<ExcursionOffer> options)
    {
        var user = GetUser().Value!;
        var response = await this._query.Get(user);

        return OdataResponse(response, options);
    }

    [HttpGet("{key}")]
    public async Task<IActionResult> Get([FromODataUri] int key, ODataQueryOptions<ExcursionOffer> options)
    {
        var user = GetUser().Value!;
        var response = await this._query.Get(user);

        return OdataSingleResponse(response, options, x => x.Id == key);
    }


    [HttpPost]
    public async Task<IActionResult> CreateExcursionOffer([FromBody] ExcursionOfferRequest excursionOffer)
    {
        var user = GetUser().Value;
        return ToResponse(await _excursionOfferService.CreateOffer(excursionOffer, user!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExcursionOffer(int id, [FromBody] ExcursionOfferRequest excursionOffer)
    {
        var user = GetUser().Value;
        return ToResponse(await _excursionOfferService.UpdateOffer(id, excursionOffer, user!));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExcursionOffer(int id)
    {
        var user = GetUser().Value;
        return ToResponse(await _excursionOfferService.DeleteOffer(id, user!));
    }
}