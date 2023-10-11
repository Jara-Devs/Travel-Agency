using Travel_Agency_Domain.Services;

namespace Travel_Agency_Domain.Offers;

public class FlightOffer : Offer
{
    public int FlightId { get; set; }

    public Flight Flight { get; set; } = null!;

    public FlightOffer(string description, double price,int flightId) : base(description, price)
    {
        this.FlightId = flightId;
    }
}