using Microsoft.EntityFrameworkCore;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Services
{
    public class ExcursionService
    {
        private readonly TravelAgencyContext _context;

        public ExcursionService(TravelAgencyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Excursion>> GetAllExcursionsAsync()
        {
            return await _context.Excursions.ToListAsync();
        }

        public async Task<Excursion?> GetExcursionByIdAsync(int id)
        {
            return await _context.Excursions.FindAsync(id);
        }

        public async Task CreateExcursionAsync(Excursion excursion)
        {
            _context.Excursions.Add(excursion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateExcursionAsync(Excursion excursion)
        {
            _context.Entry(excursion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExcursionAsync(int id)
        {
            var excursion = await _context.Excursions.FindAsync(id);
            _context.Excursions.Remove(excursion!);
            await _context.SaveChangesAsync();
        }
    }
}