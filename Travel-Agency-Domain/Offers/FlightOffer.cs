using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Domain.Offers;

public class FlightOffer : Offer
{
    public Guid FlightId { get; set; }

    public Flight Flight { get; set; } = null!;

    public List<FlightFacility> Facilities { get; set; }

    public FlightOffer(string name, int availability, string description, double price, long startDate,
        long endDate, Guid agencyId, Guid flightId, List<FlightFacility> facilities, Guid imageId)
        : base(description, price, name, availability, startDate, endDate, agencyId, imageId, OfferType.Flight)
    {
        this.FlightId = flightId;
        this.Facilities = facilities;
    }
}