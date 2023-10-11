using Travel_Agency_Core;
using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_Domain.Services;

public class Flight : Entity
{
    public string Company { get; set; }

    public FlightCategory FlightCategory { get; set; }

    public long Duration { get; set; }

    public int Place1Id { get; set; }
    public TouristPlace Place1 { get; set; } = null!;

    public int Place2Id { get; set; }
    public TouristPlace Place2 { get; set; } = null!;

    public Flight(string company, FlightCategory flightCategory, long duration, int place1Id, int place2Id)
    {
        this.Company = company;
        this.FlightCategory = flightCategory;
        this.Duration = duration;
        this.Place1Id = place1Id;
        this.Place2Id = place2Id;
    }

    public ICollection<FlightOffer> Offers { get; set; } = null!;
}