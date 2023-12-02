using Travel_Agency_Core;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_Domain.Services;

public class Flight : Entity
{
    public Flight(string company, long duration, Guid originId, Guid destinationId)
    {
        Company = company;
        Duration = duration;
        OriginId = originId;
        DestinationId = destinationId;
    }

    public string Company { get; set; }

    public long Duration { get; set; }

    public Guid OriginId { get; set; }

    public City Origin { get; set; } = null!;

    public Guid DestinationId { get; set; }

    public City Destination { get; set; } = null!;

    public ICollection<FlightOffer> Offers { get; set; } = null!;
}