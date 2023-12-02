using Travel_Agency_Core;
using Travel_Agency_Domain.Images;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Domain;

public class City : Entity
{
    public string Name { get; set; }

    public string Country { get; set; }

    public City(string name, string country, Guid imageId)
    {
        this.Name = name;
        this.Country = country;
        this.ImageId = imageId;
    }

    public Guid ImageId { get; set; }

    public Image Image { get; set; } = null!;

    public ICollection<TouristPlace> TouristPlaces { get; set; } = null!;

    public ICollection<Flight> OriginFlights { get; set; } = null!;

    public ICollection<Flight> DestinationFlights { get; set; } = null!;
}