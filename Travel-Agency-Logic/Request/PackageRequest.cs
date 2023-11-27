using Travel_Agency_Domain.Packages;

namespace Travel_Agency_Logic.Request;

public class PackageRequest
{
    public double Discount { get; set; }

    public string Description { get; set; } = null!;

    public ICollection<Guid> HotelOffers { get; set; } = null!;
    public ICollection<Guid> ExcursionOffers { get; set; } = null!;
    public ICollection<Guid> FlightOffers { get; set; } = null!;

    public Package Package(Package? package = null)
    {
        package ??= new Package(this.Description, this.Discount);
        package.Description = this.Description;
        package.Discount = this.Discount;

        return package;
    }
}