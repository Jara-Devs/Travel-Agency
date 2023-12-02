using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Travel_Agency_Api.Core;
using Travel_Agency_Core;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Payments;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;

namespace Travel_Agency_Api.Controllers.Reserves;

[ApiController]
[Route("[controller]")]
public class ReserveTouristController : TravelAgencyController
{
    private readonly IQueryEntity<ReserveTourist> _query;
    private readonly IReserveService<ReserveTourist, PaymentOnline> _reserveTouristService;

    public ReserveTouristController
        (IReserveService<ReserveTourist, PaymentOnline> reserveTouristService, IQueryEntity<ReserveTourist> query)
    {
        _reserveTouristService = reserveTouristService;
        _query = query;
    }

    [HttpGet]
    [Authorize(Policy = Policies.ReserveTouristAgency)]
    public async Task<IActionResult> Get(ODataQueryOptions<ReserveTourist> options)
    {
        var user = GetUser().Value!;
        var response = await _query.Get(user);

        return OdataResponse(response, options);
    }

    [HttpGet("{key}")]
    [Authorize(Policy = Policies.ReserveTouristAgency)]
    public async Task<IActionResult> Get([FromODataUri] Guid key, ODataQueryOptions<ReserveTourist> options)
    {
        var user = GetUser().Value!;
        var response = await _query.Get(user);

        return OdataSingleResponse(response, options, x => x.Id == key);
    }


    [HttpPost]
    [Authorize(Roles = Roles.Tourist)]
    public async Task<IActionResult> CreateReserveTourist([FromBody] ReserveTouristRequest reserveTourist)
    {
        var user = GetUser().Value;
        return ToResponse(await _reserveTouristService.CreateReserve(reserveTourist, user!));
    }
}