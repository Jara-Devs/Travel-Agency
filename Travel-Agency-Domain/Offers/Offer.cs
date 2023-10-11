using Travel_Agency_Core;

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
}