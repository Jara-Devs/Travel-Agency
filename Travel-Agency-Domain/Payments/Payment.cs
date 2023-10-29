using Travel_Agency_Core;
using Travel_Agency_Domain.Others;

namespace Travel_Agency_Domain.Payments;

public class Payment : Entity
{
    public int OrderId { get; set; }

    public PaymentOrder Order { get; set; } = null!;

    public UserIdentity UserIdentity { get; set; }

    public Payment(int orderId, UserIdentity userIdentity)
    {
        this.OrderId = orderId;
        this.UserIdentity = userIdentity;
    }
}