using Travel_Agency_Core;
using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_Domain.Services;

public class Flight : Entity
{
    public string Company { get; set; }

    public long Duration { get; set; }

    public int OriginId { get; set; }
    
    public TouristPlace Origin { get; set; } = null!;

    public int DestinationId { get; set; }

    public TouristPlace Destination { get; set; } = null!;

    public Flight(string company, long duration, int originId, int destinationId)
    {
        this.Company = company;
        this.Duration = duration;
        this.OriginId = originId;
        this.DestinationId = destinationId;
    }

    public ICollection<FlightOffer> Offers { get; set; } = null!;
}