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
        flight ??= new Flight(Company, Duration, OriginId, DestinationId);
        flight.Company = Company;
        flight.Duration = Duration;
        flight.OriginId = OriginId;
        flight.DestinationId = DestinationId;

        return flight;
    }
}