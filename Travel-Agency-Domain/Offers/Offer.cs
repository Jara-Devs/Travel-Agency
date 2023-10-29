using Travel_Agency_Core;
using Travel_Agency_Domain.Packages;

namespace Travel_Agency_Domain.Offers;

public abstract class Offer : Entity
{
    public string Description { get; set; }

    public double Price { get; set; }

    public Offer(string description, double price)
    {
        this.Description = description;
        this.Price = price;
    }

    public ICollection<Package> Packages { get; set; } = null!;
}