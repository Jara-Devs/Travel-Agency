using Microsoft.AspNetCore.Http;
using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Images;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Request;

public class ImageRequest
{
    public IFormFile? File { get; set; }

    public static Image Image(string name, string url) => new(name, url);
}