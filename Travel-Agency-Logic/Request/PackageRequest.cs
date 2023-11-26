using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Packages;

namespace Travel_Agency_Logic.Request;

public class PackageRequest
{
    public ReactionState ReactionState { get; set; }

    public double Discount { get; set; }

    public string Description { get; set; } = null!;

    public ICollection<int> Offers { get; set; } = null!;

    public Package Package(Package? package = null)
    {
        package ??= new Package(this.ReactionState, this.Description, this.Discount);
        package.Description = this.Description;
        package.Discount = this.Discount;

        return package;
    } 
}