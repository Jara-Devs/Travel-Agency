namespace Travel_Agency_Api.Models.Entities;

public class Address
{
    public string Country { get; set; }

    public string City { get; set; }

    public string Description { get; set; }

    public Address(string country, string city, string description)
    {
        this.Country = country;
        this.Description = description;
        this.City = city;
    }
}