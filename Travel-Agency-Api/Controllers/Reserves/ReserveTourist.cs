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

namespace Travel_Agency_Api.Controllers.Offers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles.Tourist)]
public class ReserveTouristController : TravelAgencyController
{
    private readonly IReserveService<ReserveTourist, PaymentOnline> _reserveTouristService;

    private readonly IQueryEntity<ReserveTourist> _query;

    public ReserveTouristController
        (IReserveService<ReserveTourist, PaymentOnline> reserveTouristService,IQueryEntity<ReserveTourist> query)
    {
        _reserveTouristService = reserveTouristService;
        _query = query;
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get(ODataQueryOptions<ReserveTourist> options)
    {
        var user = GetUser().Value!;
        var response = await this._query.Get(user);

        return OdataResponse(response, options);
    }

    [HttpGet("{key}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromODataUri] int key, ODataQueryOptions<ReserveTourist> options)
    {
        var user = GetUser().Value!;
        var response = await this._query.Get(user);

        return OdataSingleResponse(response, options, x => x.Id == key);
    }


    [HttpPost]
    public async Task<IActionResult> CreateReserveTourist([FromBody] ReserveTouristRequest reserveTourist)
    {
        var user = GetUser().Value;
        return ToResponse(await _reserveTouristService.CreateReserve(reserveTourist, user!));
    }
}