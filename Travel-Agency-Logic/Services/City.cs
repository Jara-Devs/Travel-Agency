using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Services;

public class CityService : ICityService
{
    private readonly TravelAgencyContext _context;

    public CityService(TravelAgencyContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<IdResponse>> CreateCity(CityRequest cityRequest, UserBasic userBasic)
    {
        if (!Helpers.CheckPermissions(userBasic))
            return new Unauthorized<IdResponse>("You don't have permissions");

        if (await _context.Cities.AnyAsync(c => c.Name == cityRequest.Name && c.Country == cityRequest.Country))
            return new BadRequest<IdResponse>("The city with same name and country already exists");

        var city = cityRequest.City();
        _context.Cities.Add(city);
        await _context.SaveChangesAsync();

        return new ApiResponse<IdResponse>(new IdResponse { Id = city.Id });
    }

    public async Task<ApiResponse> UpdateCity(Guid id, CityRequest cityRequest, UserBasic userBasic)
    {
        if (!Helpers.CheckPermissions(userBasic))
            return new Unauthorized("You don't have permissions");

        var city = await _context.Cities.FindAsync(id);
        if (city is null) return new NotFound("City not found");

        if (await _context.Cities.AnyAsync(c =>
                c.Id != id && c.Name == cityRequest.Name && c.Country == cityRequest.Country))
            return new BadRequest("The city with same name and country already exists");

        var inUse = await CheckDependency(id);
        if (!inUse.Ok) return inUse;

        city = cityRequest.City(city);

        _context.Cities.Update(city);
        await _context.SaveChangesAsync();

        return new ApiResponse();
    }

    public async Task<ApiResponse> DeleteCity(Guid id, UserBasic userBasic)
    {
        if (!Helpers.CheckPermissions(userBasic))
            return new Unauthorized("You don't have permissions");

        var city = await _context.Cities.FindAsync(id);
        if (city is null) return new NotFound("City not found");

        var inUse = await CheckDependency(id);
        if (!inUse.Ok) return inUse;

        _context.Cities.Remove(city);
        await _context.SaveChangesAsync();

        return new ApiResponse();
    }

    private async Task<ApiResponse> CheckDependency(Guid id)
    {
        if (await _context.TouristPlaces.AnyAsync(tp => tp.CityId == id))
            return new BadRequest("The city is in use");

        if (await _context.Flights.AnyAsync(f => f.OriginId == id || f.DestinationId == id))
            return new BadRequest("The city is in use");

        return new ApiResponse();
    }
}