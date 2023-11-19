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
            var count = await _context.Images.CountAsync();
            var list = GenerateRandom(count);

            var images = await _context.Images.Where(x => list.Contains(x.Id)).ToListAsync();

            return new ApiResponse<ICollection<Image>>(images);
        }

        private IEnumerable<int> GenerateRandom(int count)
        {
            const int cant = 11;
            var cantIter = 100000;

            var set = new HashSet<int>();

            if (count <= cant)
            {
                for (var i = 1; i <= count; i++)
                    set.Add(i);

                return set;
            }

            var rnd = new Random();
            while (set.Count != cant && cantIter != 0)
            {
                var value = rnd.Next(1, count + 1);
                set.Add(value);
                cantIter--;
            }

            return set;
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