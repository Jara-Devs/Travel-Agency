using Travel_Agency_Domain.Others;
using Travel_Agency_Domain.Users;

namespace Travel_Agency_Domain.Payments;

public class ReserveTicket : Reserve
{
    public ReserveTicket()
    {
    }

    public ReserveTicket(Guid packageId, ICollection<UserIdentity> userIdentities, Guid userId, Guid paymentId) : base(
        packageId,
        userIdentities)
    {
        UserId = userId;
        PaymentId = paymentId;
    }

    public UserAgency User { get; set; } = null!;

    public Guid PaymentId { get; set; }

    public PaymentTicket Payment { get; set; } = null!;
}