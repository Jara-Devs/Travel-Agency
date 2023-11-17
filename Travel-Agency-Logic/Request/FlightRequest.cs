using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Request;

public class FlightRequest
{
    public string Company { get; set; } = null!;

    public FlightCategory FlightCategory { get; set; }

    public long Duration { get; set; } = 0;

    public int OriginId { get; set; } = 0;

    public int DestinationId { get; set; } = 0;

    public Flight Flight(Flight? flight = null) 
    {
        flight ??= new Flight(this.Company, this.FlightCategory, this.Duration, this.OriginId, this.DestinationId);
        flight.Company = this.Company;
        flight.FlightCategory = this.FlightCategory;
        flight.Duration = this.Duration;
        flight.OriginId = this.OriginId;
        flight.DestinationId = this.DestinationId;

        return flight;
    }
}