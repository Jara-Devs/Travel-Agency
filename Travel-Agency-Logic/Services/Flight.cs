using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Services;

public class FlightService : IFlightService
{
    private readonly TravelAgencyContext _context;

    public FlightService(TravelAgencyContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<IdResponse>> CreateFlight(FlightRequest flight, UserBasic user)
    {
        if (!Helpers.CheckPermissions(user))
            return new Unauthorized<IdResponse>("You don't have permissions");

        if (await CheckIfFlightExists(flight))
            return new NotFound<IdResponse>("The flight already exists");

        var isCoherent = await CheckCoherence(flight);
        if (!isCoherent.Ok) return isCoherent.ConvertApiResponse<IdResponse>();

        var entity = flight.Flight();
        _context.Flights.Add(entity);
        await _context.SaveChangesAsync();

        return new ApiResponse<IdResponse>(new IdResponse { Id = entity.Id });
    }

    public async Task<ApiResponse> UpdateFlight(Guid id, FlightRequest flightRequest, UserBasic user)
    {
        if (!Helpers.CheckPermissions(user))
            return new Unauthorized("You don't have permissions");

        var flight = await _context.Flights.FindAsync(id);
        if (flight is null)
            return new NotFound("Flight not found");

        if (await CheckIfFlightExists(flightRequest))
            return new NotFound("The flight already exists");

        var isCoherent = await CheckCoherence(flightRequest);
        if (!isCoherent.Ok) return isCoherent;

        var inUse = await CheckDependency(id);
        if (!inUse.Ok) return inUse;

        var newFlight = flightRequest.Flight(flight);
        newFlight.Id = id;

        _context.Update(newFlight);
        await _context.SaveChangesAsync();

        return new ApiResponse();
    }

    public async Task<ApiResponse> DeleteFlight(Guid id, UserBasic user)
    {
        if (!Helpers.CheckPermissions(user))
            return new Unauthorized("You don't have permissions");

        var inUse = await CheckDependency(id);
        if (!inUse.Ok) return inUse;

        var flight = await _context.Flights.FindAsync(id);

        if (flight is null)
            return new NotFound("Hotel not found");

        _context.Flights.Remove(flight);
        await _context.SaveChangesAsync();
        return new ApiResponse();
    }

    private async Task<ApiResponse> CheckDependency(Guid id)
    {
        if (await _context.FlightOffers.AnyAsync(o => o.FlightId == id))
            return new BadRequest("There is an offer for this flight");

        return new ApiResponse();
    }

    private async Task<ApiResponse> CheckCoherence(FlightRequest flight)
    {
        if (flight.OriginId == flight.DestinationId)
            return new BadRequest("Origin and destination can't be the same");
        var origin = await _context.Cities.FindAsync(flight.OriginId);
        if (origin is null)
            return new NotFound("Origin not found");
        var destination = await _context.Cities.FindAsync(flight.DestinationId);
        if (destination is null)
            return new NotFound("Destination not found");
        if (flight.Duration <= 0)
            return new BadRequest("Duration can't be less than 0");

        return new ApiResponse();
    }

    private async Task<bool> CheckIfFlightExists(FlightRequest flight, Guid? id = null)
    {
        return await _context.Flights.AnyAsync(f =>
            f.Company == flight.Company &&
            f.OriginId == flight.OriginId &&
            f.DestinationId == flight.DestinationId && id != null && id == f.Id
        );
    }
}