using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Domain.Offers;

public class FlightOffer : Offer
{
    public int FlightId { get; set; }

    public Flight Flight { get; set; } = null!;

    public List<FlightFacility> Facilities { get; set; } = null!;

    public FlightOffer(string name, int availability, string description, double price, long startDate,
        long endDate, int agencyId)
        : base(description, price, name, availability, startDate, endDate, agencyId)
    {
    }

    public FlightOffer(string name, int availability, string description, double price, long startDate,
        long endDate, int agencyId, int flightId, List<FlightFacility> facilities)
        : base(description, price, name, availability, startDate, endDate, agencyId)
    {
        this.FlightId = flightId;
        this.Facilities = facilities;
    }
}