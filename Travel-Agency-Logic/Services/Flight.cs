using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Services;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Services
{
    public class FlightService : IFlightService
    {
        private readonly TravelAgencyContext _context;

        public FlightService(TravelAgencyContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IdResponse>> CreateFlight(FlightRequest flight, UserBasic user)
        {
            if (!CheckPermissions(user))
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

        public async Task<ApiResponse> UpdateFlight(int id, FlightRequest flightRequest, UserBasic user)
        {
            if (!CheckPermissions(user))
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

        public async Task<ApiResponse> DeleteFlight(int id, UserBasic user)
        {
            if (!CheckPermissions(user))
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

        private static bool CheckPermissions(UserBasic user) =>
            user.Role == Roles.AdminApp || user.Role == Roles.EmployeeApp;

        private async Task<ApiResponse> CheckDependency(int id)
        {
            if (await this._context.FlightOffers.AnyAsync(o => o.FlightId == id))
                return new BadRequest("There is an offer for this flight");

            return new ApiResponse();
        }

        private async Task<ApiResponse> CheckCoherence(FlightRequest flight)
        {
            if (flight.OriginId == flight.DestinationId)
                return new BadRequest("Origin and destination can't be the same");
            var origin = await _context.TouristPlaces.FindAsync(flight.OriginId);
            if (origin is null)
                return new NotFound("Origin not found");
            var destination = await _context.TouristPlaces.FindAsync(flight.DestinationId);
            if (destination is null)
                return new NotFound("Destination not found");
            if (flight.Duration < 0)
                return new BadRequest("Duration can't be negative");

            return new ApiResponse();
        }

        private async Task<bool> CheckIfFlightExists(FlightRequest flight) 
            => await _context.Flights.AnyAsync(f =>
                f.Company == flight.Company &&
                f.OriginId == flight.OriginId &&
                f.DestinationId == flight.DestinationId
            );
    }
}