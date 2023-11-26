using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Request;

public class FlightRequest
{
    public string Company { get; set; } = null!;

    public long Duration { get; set; } = 0;

    public Guid OriginId { get; set; }

    public Guid DestinationId { get; set; }

    public Flight Flight(Flight? flight = null) 
    {
        flight ??= new Flight(this.Company, this.Duration, this.OriginId, this.DestinationId);
        flight.Company = this.Company;
        flight.Duration = this.Duration;
        flight.OriginId = this.OriginId;
        flight.DestinationId = this.DestinationId;

        return flight;
    }
}