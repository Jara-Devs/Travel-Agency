using Travel_Agency_Domain.Services;

namespace Travel_Agency_Domain.Offers;

public class ExcursionOffer : Offer
{
    public int ExcursionId { get; set; }

    public Excursion Excursion { get; set; } = null!;

    public ExcursionOffer(string name, int availability, string description, double price, long startDate, 
        long endDate, int agencyId, int excursionId) 
        : base(description, price, name, availability, startDate, endDate, agencyId) {
        this.ExcursionId = excursionId;
    }
}