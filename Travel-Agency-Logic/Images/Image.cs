using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Services;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Services
{
    public class ImageService : IImageService
    {
        private readonly TravelAgencyContext _context;

        public ImageService(TravelAgencyContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IdResponse>> UploadImage(ImageRequest imageRequest)
        {
            var cloudinary = new Cloudinary(
            new Account(
                "dryboggbt", 
                "514276145235847", 
                "GSA-X91PxIeg9FvLLE53VnuCEAA"
                ));

            var file = imageRequest.File;
            if (file is null) return new BadRequest<IdResponse>("There is no file to upload");

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream())
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            var entity = imageRequest.Image(uploadResult.Url.ToString());
            _context.Images.Add(entity);
            await _context.SaveChangesAsync();

            return new ApiResponse<IdResponse>(new IdResponse { Id = entity.Id });
        }
    }
}