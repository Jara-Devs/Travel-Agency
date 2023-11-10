using Travel_Agency_Domain.Packages;

namespace Travel_Agency_Logic.Request;

public class PackageRequest
{
    public long Duration { get; set; }

    public double Discount { get; set; }

    public string Description { get; set; } = null!;

    public ICollection<int> Offers { get; set; } = null!;

    public Package Package() => new(Duration, Description, Discount);
}