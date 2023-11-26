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
[Authorize(Roles.EmployeeAgency)]
public class ReserveTicketController : TravelAgencyController
{
    private readonly IReserveService<ReserveTicket, PaymentTicket> _reserveTicketService;

    private readonly IQueryEntity<ReserveTicket> _query;

    public ReserveTicketController
        (IReserveService<ReserveTicket, PaymentTicket> reserveTicketService,IQueryEntity<ReserveTicket> query)
    {
        _reserveTicketService = reserveTicketService;
        _query = query;
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get(ODataQueryOptions<ReserveTicket> options)
    {
        var user = GetUser().Value!;
        var response = await this._query.Get(user);

        return OdataResponse(response, options);
    }

    [HttpGet("{key}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromODataUri] Guid key, ODataQueryOptions<ReserveTicket> options)
    {
        var user = GetUser().Value!;
        var response = await this._query.Get(user);

        return OdataSingleResponse(response, options, x => x.Id == key);
    }


    [HttpPost]
    public async Task<IActionResult> CreateReserveTicket([FromBody] ReserveTicketRequest reserveTicket)
    {
        var user = GetUser().Value;
        return ToResponse(await _reserveTicketService.CreateReserve(reserveTicket, user!));
    }
}