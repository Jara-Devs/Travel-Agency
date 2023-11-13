using Travel_Agency_Domain.Others;
using Travel_Agency_Domain.Users;

namespace Travel_Agency_Domain.Payments;

public class ReserveTicket : Reserve
{

    public UserAgency User { get; set; } = null!;

    public int PaymentId { get; set; }

    public PaymentTicket Payment { get; set; } = null!;

    public ReserveTicket()
    {
    }

    public ReserveTicket(int packageId, ICollection<UserIdentity> userIdentities, int userId, int paymentId) : base(
        packageId,
        userIdentities)
    {
        this.UserId = userId;
        this.PaymentId = paymentId;
    }
}