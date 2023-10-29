using Travel_Agency_Domain.Others;
using Travel_Agency_Domain.Users;

namespace Travel_Agency_Domain.Payments;

public class ReserveTourist : Reserve
{
    public int UserId { get; set; }

    public Tourist User { get; set; } = null!;

    public int PaymentId { get; set; }

    public PaymentOnline Payment { get; set; } = null!;

    public ReserveTourist()
    {
    }

    public ReserveTourist(int packageId, ICollection<UserIdentity> userIdentities, int userId, int paymentId) : base(
        packageId, userIdentities)
    {
        this.UserId = userId;
        this.PaymentId = paymentId;
    }
}