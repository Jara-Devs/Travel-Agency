using Travel_Agency_Core;
using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_Domain.Services;

public class Flight : Entity
{
    public string Company { get; set; }

    public long Duration { get; set; }

    public Guid OriginId { get; set; }
    
    public TouristPlace Origin { get; set; } = null!;

    public Guid DestinationId { get; set; }

    public TouristPlace Destination { get; set; } = null!;

    public Flight(string company, long duration, Guid originId, Guid destinationId)
    {
        this.Company = company;
        this.Duration = duration;
        this.OriginId = originId;
        this.DestinationId = destinationId;
    }

    public ICollection<FlightOffer> Offers { get; set; } = null!;
}