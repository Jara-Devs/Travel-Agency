using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Domain.Offers;

public class ExcursionOffer : Offer
{
    public int ExcursionId { get; set; }

    public Excursion Excursion { get; set; } = null!;

    public List<ExcursionFacility> Facilities { get; set; } = null!;

    public ExcursionOffer(string name, int availability, string description, double price, long startDate,
        long endDate, int agencyId)
        : base(description, price, name, availability, startDate, endDate, agencyId)
    {
    }

    public ExcursionOffer(string name, int availability, string description, double price, long startDate,
        long endDate, int agencyId, int excursionId, List<ExcursionFacility> facilities)
        : base(description, price, name, availability, startDate, endDate, agencyId)
    {
        this.ExcursionId = excursionId;
        this.Facilities = facilities;
    }
}