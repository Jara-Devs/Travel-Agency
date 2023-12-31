using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_Agency_Api.Core;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;

namespace Travel_Agency_Api.Controllers.Images;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class ImageController : TravelAgencyController
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    public async Task<IActionResult> GetRandomImages()
    {
        return ToResponse(await _imageService.GetRandomImages());
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage([FromForm] ImageRequest image)
    {
        return ToResponse(await _imageService.UploadImage(image));
    }
}