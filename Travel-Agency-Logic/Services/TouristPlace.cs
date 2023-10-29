using Microsoft.EntityFrameworkCore;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Services
{
    public class TouristPlaceService
    {
        private readonly TravelAgencyContext _context;

        public TouristPlaceService(TravelAgencyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TouristPlace>> GetAllTouristPlacesAsync()
        {
            return await _context.TouristPlaces.ToListAsync();
        }

        public async Task<TouristPlace?> GetTouristPlaceByIdAsync(int id)
        {
            return await _context.TouristPlaces.FindAsync(id);
        }

        public async Task CreateTouristPlaceAsync(TouristPlace touristPlace)
        {
            _context.TouristPlaces.Add(touristPlace);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTouristPlaceAsync(TouristPlace touristPlace)
        {
            _context.Entry(touristPlace).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTouristPlaceAsync(int id)
        {
            var touristPlace = await _context.TouristPlaces.FindAsync(id);
            _context.TouristPlaces.Remove(touristPlace!);
            await _context.SaveChangesAsync();
        }
    }
}