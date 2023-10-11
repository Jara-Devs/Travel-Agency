namespace Travel_Agency_Domain.Offers;

public abstract class Offer
{
    public int Id { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public Offer(string description, double price)
    {
        this.Description = description;
        this.Price = price;
    }
}