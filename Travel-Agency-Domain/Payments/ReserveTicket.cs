using Travel_Agency_Domain.Users;

namespace Travel_Agency_Domain.Payments;

public class ReserveTicket : Reserve
{

    public ReserveTicket(Guid packageId, Guid userId, Guid paymentId, int cant) :
        base(packageId,cant)
    {
        UserId = userId;
        PaymentId = paymentId;
    }

    public UserAgency User { get; set; } = null!;

    public PaymentTicket Payment { get; set; } = null!;
}