using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Images;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;

namespace Travel_Agency_Logic.Images
{
    public class ImageService : IImageService
    {
        private readonly TravelAgencyContext _context;

        private readonly IConfiguration _configuration;


        public ImageService(TravelAgencyContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ApiResponse<ICollection<Image>>> GetRandomImages()
        {
            const int cantPlaces = 8;
            const int cantActivities = 8;

            var imagesPlaces = await _context.TouristPlaces.Include(x => x.Image).Select(x => x.Image)
                .OrderBy(x => Guid.NewGuid() > x.Id).Take(cantPlaces).ToListAsync();

            var imagesActivities = await _context.TouristActivities.Include(x => x.Image).Select(x => x.Image)
                .OrderBy(x => Guid.NewGuid() > x.Id).Take(cantActivities).ToListAsync();

            var images = imagesPlaces.Concat(imagesActivities).ToList();

            var rnd = new Random();
            images.Sort((_, _) => rnd.Next(3) - 1);
            return new ApiResponse<ICollection<Image>>(images);
        }

        public async Task<ApiResponse<Image>> UploadImage(ImageRequest imageRequest)
        {
            try
            {
                var cloud = _configuration["Cloudinary:Cloud"];
                var apiKey = _configuration["Cloudinary:ApiKey"];
                var apiSecret = _configuration["Cloudinary:ApiSecret"];

                var cloudinary = new Cloudinary(
                    new Account(cloud, apiKey, apiSecret));

                var file = imageRequest.File;
                if (file is null) return new BadRequest<Image>("There is no file to upload");

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream())
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                var entity = ImageRequest.Image(file.FileName, uploadResult.Url.ToString());
                _context.Images.Add(entity);
                await _context.SaveChangesAsync();

                return new ApiResponse<Image>(entity);
            }
            catch
            {
                return new ApiResponse<Image>(HttpStatusCode.InternalServerError, "Connection error");
            }
        }
    }
}