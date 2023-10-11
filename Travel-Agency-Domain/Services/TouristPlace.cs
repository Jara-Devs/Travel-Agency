using Travel_Agency_Domain.Entities;

namespace Travel_Agency_Domain.Services;

public class TouristPlace
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Address Address { get; set; }

    public TouristPlace(string name, string description, Address address)
    {
        this.Name = name;
        this.Description = description;
        this.Address = address;
    }

    public ICollection<Excursion> Excursions { get; set; } = null!;

    public ICollection<Hotel> Hotels { get; set; } = null!;
}