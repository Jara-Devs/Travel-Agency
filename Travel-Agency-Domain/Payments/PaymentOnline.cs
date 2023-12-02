namespace Travel_Agency_Domain.Payments;

public class PaymentOnline : Payment
{
    public PaymentOnline(Guid userIdentityId, double price, string creditCard) : base(userIdentityId, price)
    {
        CreditCard = creditCard;
    }

    public string CreditCard { get; set; }

    public ICollection<ReserveTourist> ReserveTourists { get; set; } = null!;
}