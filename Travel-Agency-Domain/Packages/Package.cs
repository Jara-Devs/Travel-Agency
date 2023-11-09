using Travel_Agency_Core;
using Travel_Agency_Domain.Offers;
using Travel_Agency_Domain.Payments;

namespace Travel_Agency_Domain.Packages;

public class Package : Entity
{
    public long Duration { get; set; }

    public double Price() => this.Offers.Sum(o => o.Price) * (100 - this.Discount) / 100;

    public double Discount { get; set; }

    public string Description { get; set; }

    public ICollection<Offer> Offers { get; set; } = null!;

    public Package(long duration, string description, double discount = 0)
    {
        this.Duration = duration;
        this.Description = description;
        this.Discount = discount;
    }

    public ICollection<Reserve> Reserves { get; set; } = null!;
}