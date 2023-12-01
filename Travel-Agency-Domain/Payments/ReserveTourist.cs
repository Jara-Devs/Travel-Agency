using Travel_Agency_Domain.Others;
using Travel_Agency_Domain.Users;

namespace Travel_Agency_Domain.Payments;

public class ReserveTourist : Reserve
{
    public ReserveTourist()
    {
    }

    public ReserveTourist(Guid packageId, ICollection<UserIdentity> userIdentities, Guid userId, Guid paymentId) : base(
        packageId, userIdentities)
    {
        UserId = userId;
        PaymentId = paymentId;
    }

    public Tourist User { get; set; } = null!;

    public PaymentOnline Payment { get; set; } = null!;
}