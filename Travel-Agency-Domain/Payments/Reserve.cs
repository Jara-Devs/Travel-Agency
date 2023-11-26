using Travel_Agency_Core;
using Travel_Agency_Domain.Others;
using Travel_Agency_Domain.Packages;
using Travel_Agency_Domain.Users;

namespace Travel_Agency_Domain.Payments;

public class Reserve : Entity
{
    public Guid UserId { get; set; }
    
    public Guid PackageId { get; set; }

    public Package Package { get; set; } = null!;

    public ICollection<UserIdentity> UserIdentities { get; set; } = null!;

    public Reserve()
    {
    }

    public Reserve(Guid packageId, ICollection<UserIdentity> userIdentities)
    {
        this.PackageId = packageId;
        this.UserIdentities = userIdentities;
    }
}