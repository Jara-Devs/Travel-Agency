using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Travel_Agency_Api.Core;
using Travel_Agency_Core;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Reactions;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;

namespace Travel_Agency_Api.Controllers.Reactions;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = Roles.Tourist)]
public class ReactionController : TravelAgencyController
{
    private readonly IReactionService _reactionService;

    private readonly IQueryEntity<Reaction> _query;

    public ReactionController(IReactionService reactionService,
        IQueryEntity<Reaction> query)
    {
        _reactionService = reactionService;
        _query = query;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get(ODataQueryOptions<Reaction> options)
    {
        var user = GetUser().Value!;
        var response = await this._query.Get(user);

        return OdataResponse(response, options);
    }

    [HttpGet("{key}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromODataUri] int key, ODataQueryOptions<Reaction> options)
    {
        var user = GetUser().Value!;
        var response = await this._query.Get(user);

        return OdataSingleResponse(response, options, x => x.Id == key);
    }


    [HttpPost]
    public async Task<IActionResult> CreateReaction([FromBody] ReactionRequest request)
    {
        var user = GetUser().Value;
        return ToResponse(await _reactionService.CreateReaction(request, user!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReaction(int id, [FromBody] ReactionRequest request)
    {
        var user = GetUser().Value;
        return ToResponse(await _reactionService.UpdateReaction(id, request, user!));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReaction(int id)
    {
        var user = GetUser().Value;
        return ToResponse(await _reactionService.DeleteReaction(id, user!));
    }
}