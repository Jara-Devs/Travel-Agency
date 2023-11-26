using Travel_Agency_Core;
using Travel_Agency_Domain.Offers;
using Travel_Agency_Domain.Payments;

namespace Travel_Agency_Domain.Packages;

public class Package : Entity
{
    public double Price() {
        long hotel = this.HotelOffers.Sum(h => h.EndDate);
        long excursion = this.ExcursionOffers.Sum(e => e.EndDate);
        long flight = this.FlightOffers.Sum(f => f.EndDate);

        var values = new long[] {hotel, excursion, flight};

        return values.Sum() * (100 - this.Discount) / 100;
    } 

    public long StartDate() {
        long hotel = this.HotelOffers.Min(h => h.EndDate);
        long excursion = this.ExcursionOffers.Min(e => e.EndDate);
        long flight = this.FlightOffers.Min(f => f.EndDate);

        var values = new long[] {hotel, excursion, flight};

        return values.Min();
    }

    public long EndDate() {
        long hotel = this.HotelOffers.Max(h => h.EndDate);
        long excursion = this.ExcursionOffers.Max(e => e.EndDate);
        long flight = this.FlightOffers.Max(f => f.EndDate);

        var values = new long[] {hotel, excursion, flight};

        return values.Max();
    }
    public double Discount { get; set; }

    public string Description { get; set; }

    public ICollection<HotelOffer> HotelOffers { get; set; } = null!;

    public ICollection<ExcursionOffer> ExcursionOffers { get; set; } = null!;

    public ICollection<FlightOffer> FlightOffers { get; set; } = null!;

    public Package(string description, double discount = 0)
    {
        this.Description = description;
        this.Discount = discount;
    }

    public ICollection<Reserve> Reserves { get; set; } = null!;
}