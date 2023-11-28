using Travel_Agency_Core;
using Travel_Agency_Domain.Images;
using Travel_Agency_Logic.Request;

namespace Travel_Agency_Logic.Core;

public interface IImageService
{
    Task<ApiResponse<Image>> UploadImage(ImageRequest imageRequest);

    Task<ApiResponse<ICollection<Image>>> GetRandomImages();
}