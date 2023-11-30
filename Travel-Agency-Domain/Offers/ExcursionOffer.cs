using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Packages;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Domain.Offers;

public class ExcursionOffer : Offer
{
    public ExcursionOffer(string name, int availability, string description, double price, long startDate,
        long endDate, Guid agencyId, Guid excursionId, Guid imageId)
        : base(description, price, name, availability, startDate, endDate, agencyId, imageId, OfferType.Excursion)
    {
        ExcursionId = excursionId;
    }

    public Guid ExcursionId { get; set; }

    public Excursion Excursion { get; set; } = null!;

    public ICollection<Package> Packages { get; set; } = null!;
}