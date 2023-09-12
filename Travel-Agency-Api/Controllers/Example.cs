using System.Net;
using Microsoft.AspNetCore.Mvc;
using Travel_Agency_Api.Core;

namespace Travel_Agency_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class Example : TravelAgencyController
{
    [HttpGet("hello")]
    public IActionResult Hello()
    {
        return ToResponse(new ApiResponse<string>("hello"));
    }

    [HttpGet("error")]
    public IActionResult Error()
    {
        return ToResponse(new ApiResponse(HttpStatusCode.BadRequest, "Error"));
    }
    
    [HttpGet("notfound")]
    public new IActionResult NotFound()
    {
        return ToResponse(new ApiResponse(HttpStatusCode.NotFound, "NotFound"));
    }
}