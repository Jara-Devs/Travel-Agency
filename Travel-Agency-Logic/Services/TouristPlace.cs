using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Services;

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
        if (!Helpers.CheckPermissions(user))
            return new Unauthorized<IdResponse>("You don't have permissions");

        if (await _context.TouristPlaces.AnyAsync(tp => tp.Name == touristPlace.Name))
            return new NotFound<IdResponse>("The tourist place with same name already exists");

        if (!await _context.Cities.AnyAsync(c => c.Id == touristPlace.CityId))
            return new NotFound<IdResponse>("The city not found");

        var entity = touristPlace.TouristPlace();
        _context.TouristPlaces.Add(entity);
        await _context.SaveChangesAsync();

        return new ApiResponse<IdResponse>(new IdResponse { Id = entity.Id });
    }

    public async Task<ApiResponse> UpdateTouristPlace(Guid id, TouristPlaceRequest touristPlaceRequest, UserBasic user)
    {
        if (!Helpers.CheckPermissions(user))
            return new Unauthorized("You don't have permissions");

        var touristPlace = await _context.TouristPlaces.FindAsync(id);
        if (touristPlace is null) return new NotFound("Tourist place not found");

        var inUse = await CheckDependency(id);
        if (!inUse.Ok) return inUse;

        if (await _context.TouristPlaces.AnyAsync(tp => tp.Name == touristPlaceRequest.Name && tp.Id != id))
            return new NotFound("The tourist place with same name already exists");

        if (!await _context.Cities.AnyAsync(c => c.Id == touristPlace.CityId))
            return new NotFound("The city not found");

        var newTouristPlace = touristPlaceRequest.TouristPlace(touristPlace);
        newTouristPlace.Id = id;

        _context.Update(newTouristPlace);
        await _context.SaveChangesAsync();

        return new ApiResponse();
    }

    public async Task<ApiResponse> DeleteTouristPlace(Guid id, UserBasic user)
    {
        if (!Helpers.CheckPermissions(user))
            return new Unauthorized("You don't have permissions");

        var inUse = await CheckDependency(id);
        if (!inUse.Ok) return inUse;

        var touristPlace = await _context.TouristPlaces.FindAsync(id);
        if (touristPlace is null) return new NotFound("Tourist place not found");

        _context.TouristPlaces.Remove(touristPlace);
        await _context.SaveChangesAsync();

        return new ApiResponse();
    }

    private async Task<ApiResponse> CheckDependency(Guid id)
    {
        if (await _context.Hotels.AnyAsync(h => h.TouristPlaceId == id))
            return new BadRequest("There is an hotel for this tourist place");
        if (await _context.Excursions.Include(e => e.Places).AnyAsync(e => e.Places.Any(p => p.Id == id)))
            return new BadRequest("There is an excursion for this tourist place");

        return new ApiResponse();
    }
}