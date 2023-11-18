using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Travel_Agency_Api.Core;
using Travel_Agency_Core;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;

namespace Travel_Agency_Api.Controllers.Offers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class ImageController : TravelAgencyController
{
    private readonly IImageService _imageService;

    private readonly IQueryEntity<Image> _query;

    public ImageController(IImageService imageService, IQueryEntity<Image> query)
    {
        _imageService = imageService;
        _query = query;
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage([FromForm] ImageRequest image)
    {
        return ToResponse(await _imageService.UploadImage(image));
    }
}