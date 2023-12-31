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
public class ReserveTicketController : TravelAgencyController
{
    private readonly IQueryEntity<ReserveTicket> _query;
    private readonly IReserveService<ReserveTicket, PaymentTicket> _reserveTicketService;

    public ReserveTicketController
        (IReserveService<ReserveTicket, PaymentTicket> reserveTicketService, IQueryEntity<ReserveTicket> query)
    {
        _reserveTicketService = reserveTicketService;
        _query = query;
    }

    [HttpGet]
    [Authorize(Policy = Policies.ReserveTouristAgency)]
    public async Task<IActionResult> Get(ODataQueryOptions<ReserveTicket> options)
    {
        var user = GetUser().Value!;
        var response = await _query.Get(user);

        return OdataResponse(response, options);
    }

    [HttpGet("{key}")]
    [Authorize(Policy = Policies.ReserveTouristAgency)]
    public async Task<IActionResult> Get([FromODataUri] Guid key, ODataQueryOptions<ReserveTicket> options)
    {
        var user = GetUser().Value!;
        var response = await _query.Get(user);

        return OdataSingleResponse(response, options, x => x.Id == key);
    }


    [HttpPost]
    [Authorize(Policy = Policies.AgencyEmployee)]
    public async Task<IActionResult> CreateReserveTicket([FromBody] ReserveTicketRequest reserveTicket)
    {
        var user = GetUser().Value;
        return ToResponse(await _reserveTicketService.CreateReserve(reserveTicket, user!));
    }
}