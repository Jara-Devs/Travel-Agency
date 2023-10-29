using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Services;
using Travel_Agency_Logic.Core;

namespace Travel_Agency_Logic.Services
{
    public class HotelService : IHotelService
    {
        private readonly TravelAgencyContext _context;

        public HotelService(TravelAgencyContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<Hotel>> CreateHotel(Hotel hotel, UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized<Hotel>("You don't have permissions");   
            if (!await _context.Agencies.AnyAsync(a => a.Id == user.Id))
                return new NotFound<Hotel>("Hotel not found");
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return new ApiResponse<Hotel>(hotel);
        }

        public async Task<ApiResponse<Hotel>> UpdateHotel(Hotel hotel, UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized<Hotel>("You don't have permissions");  
            if (!await _context.Hotels.AnyAsync(h => h.Id == hotel.Id))
                return new NotFound<Hotel>("Hotel not found");
            _context.Update(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new ApiResponse<Hotel>(hotel);
        }

        public async Task<ApiResponse<Hotel>> DeleteHotel(int id, UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized<Hotel>("You don't have permissions");  
            if (!await _context.Hotels.AnyAsync(h => h.Id == id))
                return new NotFound<Hotel>("Hotel not found");
            var hotel = await _context.Hotels.FindAsync(id);
            _context.Hotels.Remove(hotel!);
            await _context.SaveChangesAsync();
            return new ApiResponse<Hotel>(hotel!);
        }

        private static bool CheckPermissions(UserBasic user) => user.Role == Roles.AdminApp || user.Role == Roles.EmployeeApp;
    }
}