using System.Collections;
using Travel_Agency_Core;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_Domain.Packages;

public class Package : Entity
{
    public long Duration { get; set; }

    public double Price { get; set; }

    public string Description { get; set; }

    public ICollection<Offer> Offers { get; set; } = null!;

    public Package(long duration, double price, string description)
    {
        this.Duration = duration;
        this.Description = description;
        this.Price = price;
    }
}