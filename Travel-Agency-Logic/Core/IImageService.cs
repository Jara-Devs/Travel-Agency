using Microsoft.AspNetCore.Http;
using Travel_Agency_Core;
using Travel_Agency_Domain.Images;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Core;

public interface IImageService
{
    Task<ApiResponse<Image>> UploadImage(ImageRequest imageRequest);
}