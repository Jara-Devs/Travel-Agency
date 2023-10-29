using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Services;
using Travel_Agency_Logic.Core;

namespace Travel_Agency_Logic.Services
{
    public class TouristPlaceService : ITouristPlaceService
    {
        private readonly TravelAgencyContext _context;

        public TouristPlaceService(TravelAgencyContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<TouristPlace>> CreateTouristPlace(TouristPlace touristPlace, UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized<TouristPlace>("You don't have permissions");   
            if (!await _context.Agencies.AnyAsync(a => a.Id == user.Id))
                return new NotFound<TouristPlace>("TouristPlace not found");
            _context.TouristPlaces.Add(touristPlace);
            await _context.SaveChangesAsync();
            return new ApiResponse<TouristPlace>(touristPlace);
        }

        public async Task<ApiResponse<TouristPlace>> UpdateTouristPlace(TouristPlace touristPlace, UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized<TouristPlace>("You don't have permissions");  
            if (!await _context.TouristPlaces.AnyAsync(h => h.Id == touristPlace.Id))
                return new NotFound<TouristPlace>("TouristPlace not found");
            _context.Update(touristPlace).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new ApiResponse<TouristPlace>(touristPlace);
        }

        public async Task<ApiResponse<TouristPlace>> DeleteTouristPlace(int id, UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized<TouristPlace>("You don't have permissions");  
            if (!await _context.TouristPlaces.AnyAsync(h => h.Id == id))
                return new NotFound<TouristPlace>("TouristPlace not found");
            var touristPlace = await _context.TouristPlaces.FindAsync(id);
            _context.TouristPlaces.Remove(touristPlace!);
            await _context.SaveChangesAsync();
            return new ApiResponse<TouristPlace>(touristPlace!);
        }

        private static bool CheckPermissions(UserBasic user) => user.Role == Roles.AdminApp || user.Role == Roles.EmployeeApp;
    }
}