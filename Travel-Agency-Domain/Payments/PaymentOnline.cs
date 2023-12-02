namespace Travel_Agency_Domain.Payments;

public class PaymentOnline : Payment
{
    public PaymentOnline(Guid userIdentityId, double price, long creditCard) : base(userIdentityId, price)
    {
        CreditCard = creditCard;
    }

    public long CreditCard { get; set; }

    public ICollection<ReserveTourist> ReserveTourists { get; set; } = null!;
}