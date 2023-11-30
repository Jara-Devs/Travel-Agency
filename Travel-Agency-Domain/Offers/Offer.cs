using Travel_Agency_Core;
using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Images;
using Travel_Agency_Domain.Reactions;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Domain.Offers;

public class Offer : Entity
{
    public Offer(string description, double price, string name, int availability, long startDate, long endDate,
        Guid agencyId, Guid imageId, OfferType type)
    {
        Description = description;
        Price = price;
        Name = name;
        Availability = availability;
        StartDate = startDate;
        EndDate = endDate;
        AgencyId = agencyId;
        ImageId = imageId;
        Type = type;
        Facilities = new List<Facility>();
    }

    public string Name { get; set; }

    public int Availability { get; set; }

    public Guid AgencyId { get; set; }

    public Agency Agency { get; set; } = null!;

    public string Description { get; set; }

    public double Price { get; set; }

    public long StartDate { get; set; }

    public long EndDate { get; set; }

    public Image Image { get; set; } = null!;

    public Guid ImageId { get; set; }

    public OfferType Type { get; set; }

    public ICollection<Facility> Facilities { get; set; }

    public ICollection<Reaction> Reactions { get; set; } = null!;
}