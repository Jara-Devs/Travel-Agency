using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Services;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Services
{
    public class HotelService : IHotelService
    {
        private readonly TravelAgencyContext _context;

        public HotelService(TravelAgencyContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IdResponse>> CreateHotel(HotelRequest hotel, UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized<IdResponse>("You don't have permissions");

            if (await _context.Hotels.AnyAsync(h => h.Name == hotel.Name))
                return new NotFound<IdResponse>("The hotel with same name already exists");

            var response = await CreateHotel(hotel);
            if (!response.Ok) return response.ConvertApiResponse<IdResponse>();

            var entity = response.Value!;
            _context.Hotels.Add(entity);
            await _context.SaveChangesAsync();

            return new ApiResponse<IdResponse>(new IdResponse { Id = entity.Id });
        }

        public async Task<ApiResponse> UpdateHotel(Guid id, HotelRequest hotelRequest, UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized("You don't have permissions");

            var hotel = await _context.Hotels.Include(x => x.TouristPlace).Where(x => x.Id == id).FirstOrDefaultAsync();
            if (hotel is null)
                return new NotFound("Hotel not found");

            if (await _context.Hotels.AnyAsync(h => h.Name == hotel.Name && id != h.Id))
                return new NotFound("The hotel with same name already exists");

            var inUse = await CheckDependency(id);
            if (!inUse.Ok) return inUse;

            var response = await CreateHotel(hotelRequest, hotel);
            if (!response.Ok) return response.ConvertApiResponse();

            var newHotel = response.Value!;
            newHotel.Id = id;

            _context.Update(newHotel);
            await _context.SaveChangesAsync();

            return new ApiResponse();
        }

        public async Task<ApiResponse> DeleteHotel(Guid id, UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized("You don't have permissions");

            var inUse = await CheckDependency(id);
            if (!inUse.Ok) return inUse;

            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel is null)
                return new NotFound("Hotel not found");

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return new ApiResponse();
        }

        private static bool CheckPermissions(UserBasic user) =>
            user.Role == Roles.AdminApp || user.Role == Roles.EmployeeApp;

        private async Task<ApiResponse<Hotel>> CreateHotel(HotelRequest request, Hotel? hotel = null)
        {
            hotel = request.Hotel(hotel);
            var touristPlace = await this._context.TouristPlaces.FindAsync(request.TouristPlaceId);
            if (touristPlace is null) return new BadRequest<Hotel>("Not found tourist place");

            hotel.TouristPlace = touristPlace;

            return new ApiResponse<Hotel>(hotel);
        }

        private async Task<ApiResponse> CheckDependency(Guid id)
        {
            if (await this._context.HotelOffers.AnyAsync(o => o.HotelId == id))
                return new BadRequest("There is an offer for this hotel");
            if (await this._context.Hotels.Include(x => x.OverNightExcursions).Select(x => x.OverNightExcursions)
                    .CountAsync() != 0)
                return new BadRequest("There is an over night excursion for this hotel");

            return new ApiResponse();
        }
    }
}