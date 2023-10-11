using Microsoft.AspNetCore.Mvc;
using Travel_Agency_Core;

namespace Travel_Agency_Api.Core;

public abstract class TravelAgencyController : ControllerBase
{
    protected IActionResult ToResponse<T>(ApiResponse<T> response)
    {
        if (response.Value is not null)
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
}