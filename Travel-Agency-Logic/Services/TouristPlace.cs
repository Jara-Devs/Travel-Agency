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

        public async Task<ApiResponse<IdResponse>> CreateTouristPlace(TouristicPlaceRequest touristPlace,
            UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized<IdResponse>("You don't have permissions");

            if (await _context.TouristPlaces.AnyAsync(a => a.Name == touristPlace.Name))
                return new NotFound<IdResponse>("The tourist place already exists");

            _context.TouristPlaces.Add(touristPlace.TouristPlace());
            await _context.SaveChangesAsync();

            return new ApiResponse<IdResponse>((await this._context.TouristPlaces
                .Where(x => x.Name == touristPlace.Name).Select(x => new IdResponse { Id = x.Id })
                .SingleOrDefaultAsync())!);
        }

        public async Task<ApiResponse> UpdateTouristPlace(int id, TouristicPlaceRequest touristPlace, UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized("You don't have permissions");

            if (!await _context.TouristPlaces.AnyAsync(h => h.Id == id))
                return new NotFound("Tourist place not found");

            var newTouristPlace = touristPlace.TouristPlace();
            newTouristPlace.Id = id;

            _context.Update(newTouristPlace);
            await _context.SaveChangesAsync();

            return new ApiResponse();
        }

        public async Task<ApiResponse> DeleteTouristPlace(int id, UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized("You don't have permissions");

            var touristPlace = await _context.TouristPlaces.FindAsync(id);
            if (touristPlace is null) return new NotFound("Tourist place not found");

            _context.TouristPlaces.Remove(touristPlace!);
            await _context.SaveChangesAsync();

            return new ApiResponse();
        }

        private static bool CheckPermissions(UserBasic user) =>
            user.Role == Roles.AdminApp || user.Role == Roles.EmployeeApp;
    }
}