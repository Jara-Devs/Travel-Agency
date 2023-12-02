using Travel_Agency_Domain.Users;

namespace Travel_Agency_Domain.Payments;

public class ReserveTourist : Reserve
{
    public ReserveTourist(Guid packageId, Guid userId, Guid paymentId, int cant) : base(
        packageId, cant)
    {
        UserId = userId;
        PaymentId = paymentId;
    }

    public Tourist User { get; set; } = null!;

    public PaymentOnline Payment { get; set; } = null!;
}