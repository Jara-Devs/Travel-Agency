using Travel_Agency_Domain.Services;

namespace Travel_Agency_Domain.Offers;

public class ExcursionOffer : Offer
{
    public int ExcursionId { get; set; }

    public Excursion Excursion { get; set; } = null!;

    public ExcursionOffer(string description, double price, int excursionId) : base(description, price)
    {
        this.ExcursionId = excursionId;
    }
}