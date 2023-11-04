using Travel_Agency_Domain.Services;

namespace Travel_Agency_Domain.Offers;

public class FlightOffer : Offer
{
    public int FlightId { get; set; }

    public Flight Flight { get; set; } = null!;

    public FlightOffer(string name, int availability, string description, double price, long startDate, 
        long endDate, int agencyId, int flightId) 
        : base(description, price, name, availability, startDate, endDate, agencyId) {
        this.FlightId = flightId;
    }
}