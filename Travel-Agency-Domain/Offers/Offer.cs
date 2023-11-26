using Travel_Agency_Core;
using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Images;
using Travel_Agency_Domain.Packages;
using Travel_Agency_Domain.Reactions;

namespace Travel_Agency_Domain.Offers;

public class Offer : Entity
{
    public string Name { get; set; }

    public int Availability { get; set; }

    public Guid AgencyId { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public long StartDate { get; set; }

    public long EndDate { get; set; }

    public Image Image { get; set; } = null!;

    public Guid ImageId { get; set; }

    public OfferType Type { get; set; }

    public Offer(string description, double price, string name, int availability, long startDate, long endDate,
        Guid agencyId, Guid imageId, OfferType type)
    {
        this.Description = description;
        this.Price = price;
        this.Name = name;
        this.Availability = availability;
        this.StartDate = startDate;
        this.EndDate = endDate;
        this.AgencyId = agencyId;
        this.ImageId = imageId;
        this.Type = type;
    }

    public ICollection<Package> Packages { get; set; } = null!;

    public ICollection<Reaction> Reactions { get; set; } = null!;
}