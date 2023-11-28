using Microsoft.AspNetCore.Http;
using Travel_Agency_Domain.Images;

namespace Travel_Agency_Logic.Request;

public class ImageRequest
{
    public IFormFile? File { get; set; }

    public static Image Image(string name, string url)
    {
        return new Image(name, url);
    }
}