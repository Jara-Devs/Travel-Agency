using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Services
{
    public class TouristPlaceService : ITouristPlaceService
    {
        private readonly TravelAgencyContext _context;

        public TouristPlaceService(TravelAgencyContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IdResponse>> CreateTouristPlace(TouristPlaceRequest touristPlace,
            UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized<IdResponse>("You don't have permissions");

            if (await _context.TouristPlaces.AnyAsync(tp => tp.Name == touristPlace.Name))
                return new NotFound<IdResponse>("The tourist place with same name already exists");

            var entity = touristPlace.TouristPlace();
            _context.TouristPlaces.Add(entity);
            await _context.SaveChangesAsync();

            return new ApiResponse<IdResponse>(new IdResponse { Id = entity.Id });
        }

        public async Task<ApiResponse> UpdateTouristPlace(int id, TouristPlaceRequest touristPlaceRequest, UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized("You don't have permissions");

            var touristPlace = await _context.TouristPlaces.FindAsync(id);
            if (touristPlace is null) return new NotFound("Tourist place not found");

            var inUse = await CheckDependency(id);
            if (!inUse.Ok) return inUse;

            if (await _context.TouristPlaces.AnyAsync(tp => tp.Name == touristPlaceRequest.Name && tp.Id != id))
                return new NotFound("The tourist place with same name already exists");

            var newTouristPlace = touristPlaceRequest.TouristPlace(touristPlace);
            newTouristPlace.Id = id;

            _context.Update(newTouristPlace);
            await _context.SaveChangesAsync();

            return new ApiResponse();
        }

        public async Task<ApiResponse> DeleteTouristPlace(int id, UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized("You don't have permissions");

            var inUse = await CheckDependency(id);
            if (!inUse.Ok) return inUse;

            var touristPlace = await _context.TouristPlaces.FindAsync(id);
            if (touristPlace is null) return new NotFound("Tourist place not found");

            _context.TouristPlaces.Remove(touristPlace!);
            await _context.SaveChangesAsync();

            return new ApiResponse();
        }

        private static bool CheckPermissions(UserBasic user) =>
            user.Role == Roles.AdminApp || user.Role == Roles.EmployeeApp;

        private async Task<ApiResponse> CheckDependency(int id)
        {
            if (await this._context.Hotels.AnyAsync(h => h.TouristPlaceId == id))
                return new BadRequest("There is an hotel for this tourist place");
            if (await this._context.Excursions.Include(e => e.Places).AnyAsync(e => e.Places.Any(p => p.Id == id)))
                return new BadRequest("There is an excursion for this tourist place");

            return new ApiResponse();
        }
    }
}