using Travel_Agency_Domain;

namespace Travel_Agency_Logic.Request;

public class CityRequest
{
    public string Name { get; set; } = null!;

    public string Country { get; set; } = null!;

    public Guid ImageId { get; set; }

    public City City(City? city = null)
    {
        city ??= new City(Name, Country, ImageId);
        city.Name = Name;
        city.Country = Country;
        city.ImageId = ImageId;

        return city;
    }
}