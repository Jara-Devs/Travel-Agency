using Travel_Agency_Domain.Others;

namespace Travel_Agency_Domain.Payments;

public class PaymentOnline : Payment
{
    public PaymentOnline()
    {
    }

    public PaymentOnline(UserIdentity userIdentity, double price, long creditCard) : base(userIdentity, price)
    {
        CreditCard = creditCard;
    }

    public long CreditCard { get; set; }

    public ICollection<ReserveTourist> ReserveTourists { get; set; } = null!;
}