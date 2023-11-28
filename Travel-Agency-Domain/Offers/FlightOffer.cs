using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Packages;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Domain.Offers;

public class FlightOffer : Offer
{
    public FlightOffer(string name, int availability, string description, double price, long startDate,
        long endDate, Guid agencyId, Guid flightId, List<FlightFacility> facilities, Guid imageId)
        : base(description, price, name, availability, startDate, endDate, agencyId, imageId, OfferType.Flight)
    {
        FlightId = flightId;
        Facilities = facilities;
    }

    public Guid FlightId { get; set; }

    public Flight Flight { get; set; } = null!;

    public List<FlightFacility> Facilities { get; set; }

    public ICollection<Package> Packages { get; set; } = null!;
}