using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_Agency_Api.Core;
using Travel_Agency_Core;

namespace Travel_Agency_Api.Controllers.Offers;

[ApiController]
[Route("[controller]")]
public class ImageController : TravelAgencyController
{
    public ImageController() {}

    [HttpPost]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        var cloudinary = new Cloudinary(
            new Account(
                "dryboggbt", 
                "514276145235847", 
                "GSA-X91PxIeg9FvLLE53VnuCEAA"
                ));

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, file.OpenReadStream())
        };

        var uploadResult = await cloudinary.UploadAsync(uploadParams);

        return Ok(new { url = uploadResult.SecureUrl.ToString() });
    }
}