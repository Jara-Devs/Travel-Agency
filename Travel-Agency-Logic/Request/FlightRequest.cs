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

    public Flight Flight()
        => new(Company, FlightCategory, Duration, OriginId, DestinationId);
}