using Microsoft.AspNetCore.Http;
using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Request;

public class ImageRequest
{
    public IFormFile File { get; set; } = null!;

    public Image Image(string url) => new(url);
}