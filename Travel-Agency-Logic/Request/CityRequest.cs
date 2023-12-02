using Travel_Agency_Domain;

namespace Travel_Agency_Logic.Request;

public class CityRequest
{
    public string Name { get; set; } = null!;

    public string Country { get; set; } = null!;

    public City City(City? city = null)
    {
        city ??= new City(Name, Country);
        city.Name = Name;
        city.Country = Country;

        return city;
    }
}