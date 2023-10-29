using Travel_Agency_Domain.Others;

namespace Travel_Agency_Domain.Payments;

public class PaymentOnline : Payment
{
    public long CreditCard { get; set; }

    public PaymentOnline(UserIdentity userIdentity, double price, long creditCard) : base(userIdentity, price)
    {
        this.CreditCard = creditCard;
    }

    public ICollection<ReserveTourist> ReserveTourists { get; set; } = null!;
}