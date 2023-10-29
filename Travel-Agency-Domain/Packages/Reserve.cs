using Travel_Agency_Core;
using Travel_Agency_Domain.Others;
using Travel_Agency_Domain.Users;

namespace Travel_Agency_Domain.Packages;

public class Reserve : Entity
{
    public int PackageId { get; set; }

    public Package Package { get; set; } = null!;

    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public ICollection<UserIdentity> UserIdentities { get; set; }

    public Reserve(int packageId, ICollection<UserIdentity> userIdentities)
    {
        this.PackageId = packageId;
        this.UserIdentities = userIdentities;
    }
}