using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Domain.Offers;

public class ExcursionOffer : Offer
{
    public Guid ExcursionId { get; set; }

    public Excursion Excursion { get; set; } = null!;

    public List<ExcursionFacility> Facilities { get; set; }

    public ExcursionOffer(string name, int availability, string description, double price, long startDate,
        long endDate, Guid agencyId, Guid excursionId, List<ExcursionFacility> facilities, Guid imageId)
        : base(description, price, name, availability, startDate, endDate, agencyId, imageId, OfferType.Excursion)
    {
        this.ExcursionId = excursionId;
        this.Facilities = facilities;
    }
}