namespace Travel_Agency_Domain.Others;

public class Address
{
    public Address(string country, string city, string description)
    {
        Country = country;
        Description = description;
        City = city;
    }

    public string Country { get; set; }

    public string City { get; set; }

    public string Description { get; set; }
}