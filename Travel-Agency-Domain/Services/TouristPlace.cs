using Travel_Agency_Core;
using Travel_Agency_Domain.Others;

namespace Travel_Agency_Domain.Services;

public class TouristPlace : Entity
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public Address Address { get; set; } = null!;

    public TouristPlace()
    {
    }

    public TouristPlace(string name, string description, Address address)
    {
        this.Name = name;
        this.Description = description;
        this.Address = address;
    }

    public ICollection<Excursion> Excursions { get; set; } = null!;

    public ICollection<Hotel> Hotels { get; set; } = null!;

    public ICollection<Flight> Flights1 { get; set; } = null!;

    public ICollection<Flight> Flights2 { get; set; } = null!;
}