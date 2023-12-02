using Travel_Agency_Core;
using Travel_Agency_Domain.Payments;
using Travel_Agency_Domain.Reactions;

namespace Travel_Agency_Domain.Users;

public class Tourist : User
{
    public Tourist(string name, string email, string password, Guid userIdentityId) : base(name,
        email, password, Roles.Tourist)
    {
        UserIdentityId = userIdentityId;
    }

    public Guid UserIdentityId { get; set; }

    public UserIdentity UserIdentity { get; set; } = null!;

    public ICollection<ReserveTourist> Reserves { get; set; } = null!;

    public ICollection<Reaction> Reactions { get; set; } = null!;
}