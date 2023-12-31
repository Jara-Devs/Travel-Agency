using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Travel_Agency_Core;
using Travel_Agency_Logic;

namespace Travel_Agency_Api.Core;

public abstract class TravelAgencyController : ControllerBase
{
    protected IActionResult ToResponse<T>(ApiResponse<T> response)
    {
        if (response.Value is null)
            return StatusCode((int)response.Status,
                new { ok = response.Ok, message = response.Message });

        return StatusCode((int)response.Status,
            new { ok = response.Ok, value = response.Value, message = response.Message });
    }

    protected IActionResult ToResponse(ApiResponse response)
    {
        return StatusCode((int)response.Status,
            new { ok = response.Ok, message = response.Message });
    }

    protected IActionResult OdataResponse<T>(ApiResponse<IQueryable<T>> response, ODataQueryOptions<T> options,
        Expression<Func<T, bool>>? filter = null)
    {
        return OdataResponse(response, options, filter, false);
    }

    protected IActionResult OdataSingleResponse<T>(ApiResponse<IQueryable<T>> response, ODataQueryOptions<T> options,
        Expression<Func<T, bool>>? filter = null)
    {
        return OdataResponse(response, options, filter, true);
    }

    private IActionResult OdataResponse<T>(ApiResponse<IQueryable<T>> response, ODataQueryOptions<T> options,
        Expression<Func<T, bool>>? filter, bool single)
    {
        if (response.Value is null)
            return StatusCode((int)response.Status,
                new { ok = response.Ok, message = response.Message });

        try
        {
            var query = options.ApplyTo(response.Value.Where(filter ?? (_ => true)))
                .AsQueryable() as IQueryable<object>;
            var value = single ? query!.SingleOrDefault() : query;
            if (value is null)
                return NotFound(new { ok = false, message = "Not found element" });

            return StatusCode((int)response.Status,
                new { ok = response.Ok, value, message = response.Message });
        }
        catch
        {
            return BadRequest(new { ok = false, message = "Invalid query" });
        }
    }


    protected ApiResponse<UserBasic> GetUser()
    {
        var authorization = Request.Headers["Authorization"].ToString();
        return SecurityService.DecodingAuth(authorization);
    }
}