using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Services;
using Travel_Agency_Logic.Core;

namespace Travel_Agency_Logic.Services
{
    public class ExcursionService : IExcursionService
    {
        private readonly TravelAgencyContext _context;

        public ExcursionService(TravelAgencyContext context) {
            _context = context;
        }

        public async Task<ApiResponse<Excursion>> CreateExcursion(Excursion excursion, UserBasic user) {
            if (!CheckPermissions(user))
                return new Unauthorized<Excursion>("You don't have permissions");   
            if (!await _context.Excursions.AnyAsync(a => a.Id == user.Id))
                return new NotFound<Excursion>("The excursion already exists");
            _context.Excursions.Add(excursion);
            await _context.SaveChangesAsync();
            return new ApiResponse<Excursion>(excursion);
        }

        public async Task<ApiResponse<Excursion>> UpdateExcursion(Excursion excursion, UserBasic user) {
            if (!CheckPermissions(user))
                return new Unauthorized<Excursion>("You don't have permissions");  
            if (!await _context.Excursions.AnyAsync(h => h.Id == excursion.Id))
                return new NotFound<Excursion>("Excursion not found");
            _context.Update(excursion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new ApiResponse<Excursion>(excursion);
        }

        public async Task<ApiResponse<Excursion>> DeleteExcursion(int id, UserBasic user) {
            if (!CheckPermissions(user))
                return new Unauthorized<Excursion>("You don't have permissions");  
            if (!await _context.Excursions.AnyAsync(h => h.Id == id))
                return new NotFound<Excursion>("Excursion not found");
            var excursion = await _context.Excursions.FindAsync(id);
            _context.Excursions.Remove(excursion!);
            await _context.SaveChangesAsync();
            return new ApiResponse<Excursion>(excursion!);
        }

        private static bool CheckPermissions(UserBasic user) => user.Role == Roles.AdminApp || user.Role == Roles.EmployeeApp;
    }
}