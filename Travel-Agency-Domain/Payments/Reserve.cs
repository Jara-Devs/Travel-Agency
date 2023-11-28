using Travel_Agency_Core;
using Travel_Agency_Domain.Others;
using Travel_Agency_Domain.Packages;

namespace Travel_Agency_Domain.Payments;

public class Reserve : Entity
{
    public Reserve()
    {
    }

    public Reserve(Guid packageId, ICollection<UserIdentity> userIdentities)
    {
        PackageId = packageId;
        UserIdentities = userIdentities;
    }

    public Guid UserId { get; set; }

    public Guid PackageId { get; set; }

    public Package Package { get; set; } = null!;

    public ICollection<UserIdentity> UserIdentities { get; set; } = null!;
}