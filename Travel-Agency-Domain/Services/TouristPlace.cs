using Travel_Agency_Core;
using Travel_Agency_Domain.Images;

namespace Travel_Agency_Domain.Services;

public class TouristPlace : Entity
{
    public TouristPlace(string name, string description, string address, Guid cityId, Guid imageId)
    {
        Name = name;
        Description = description;
        CityId = cityId;
        ImageId = imageId;
        Address = address;
    }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Address { get; set; }

    public City City { get; set; } = null!;

    public Guid CityId { get; set; }

    public Image Image { get; set; } = null!;

    public Guid ImageId { get; set; }

    public ICollection<Excursion> Excursions { get; set; } = null!;

    public ICollection<Hotel> Hotels { get; set; } = null!;
}