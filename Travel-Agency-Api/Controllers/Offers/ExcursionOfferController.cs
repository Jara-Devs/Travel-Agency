using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_Agency_Api.Core;
using Travel_Agency_Core;
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

    public ExcursionOfferController(IOfferService<ExcursionOffer> excursionOfferService)
    {
        _excursionOfferService = excursionOfferService;
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