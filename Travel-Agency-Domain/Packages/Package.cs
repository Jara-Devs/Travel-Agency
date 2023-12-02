using Travel_Agency_Core;
using Travel_Agency_Domain.Offers;
using Travel_Agency_Domain.Payments;

namespace Travel_Agency_Domain.Packages;

public class Package : Entity
{
    public Package(string name, string description, double discount = 0)
    {
        Name = name;
        Description = description;
        Discount = discount;
    }

    public string Name { get; set; }
    public double Discount { get; set; }

    public string Description { get; set; }

    public bool IsSingleOffer { get; set; }

    public ICollection<HotelOffer> HotelOffers { get; set; } = null!;

    public ICollection<ExcursionOffer> ExcursionOffers { get; set; } = null!;

    public ICollection<FlightOffer> FlightOffers { get; set; } = null!;

    public ICollection<Reserve> Reserves { get; set; } = null!;

    public double Price()
    {
        var hotel = HotelOffers.Sum(h => h.EndDate);
        var excursion = ExcursionOffers.Sum(e => e.EndDate);
        var flight = FlightOffers.Sum(f => f.EndDate);

        var values = new[] { hotel, excursion, flight };

        return values.Sum() * (100 - Discount) / 100;
    }

    public long StartDate()
    {
        var hotel = HotelOffers.Count == 0 ? long.MaxValue : HotelOffers.Min(h => h.EndDate);
        var excursion = ExcursionOffers.Count == 0 ? long.MaxValue : ExcursionOffers.Min(e => e.EndDate);
        var flight = FlightOffers.Count == 0 ? long.MaxValue : FlightOffers.Min(f => f.EndDate);

        var values = new[] { hotel, excursion, flight };

        return values.Min();
    }

    public long EndDate()
    {
        var hotel = HotelOffers.Count == 0 ? long.MinValue : HotelOffers.Max(h => h.EndDate);
        var excursion = ExcursionOffers.Count == 0 ? long.MaxValue : ExcursionOffers.Max(e => e.EndDate);
        var flight = FlightOffers.Count == 0 ? long.MaxValue : FlightOffers.Max(f => f.EndDate);

        var values = new[] { hotel, excursion, flight };

        return values.Max();
    }
}