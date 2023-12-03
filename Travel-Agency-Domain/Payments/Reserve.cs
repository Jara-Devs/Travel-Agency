using Travel_Agency_Core;
using Travel_Agency_Domain.Offers;
using Travel_Agency_Domain.Packages;

namespace Travel_Agency_Domain.Payments;

public class Reserve : Entity
{
    public Reserve(Guid packageId, int cant)
    {
        PackageId = packageId;
        Offers = new List<Offer>();
        Cant = cant;
    }

    public int Cant { get; set; }

    public Guid UserId { get; set; }

    public Guid PaymentId { get; set; }

    public Guid PackageId { get; set; }

    public Package Package { get; set; } = null!;

    public ICollection<UserIdentity> UserIdentities { get; set; } = null!;

    public ICollection<Offer> Offers { get; set; } = null!;
}