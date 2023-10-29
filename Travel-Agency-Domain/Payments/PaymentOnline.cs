using Travel_Agency_Domain.Others;

namespace Travel_Agency_Domain.Payments;

public class PaymentOnline : Payment
{
    public long CreditCard { get; set; }

    public PaymentOnline(int orderId, UserIdentity userIdentity, long creditCard) : base(orderId, userIdentity)
    {
        this.CreditCard = creditCard;
    }
}