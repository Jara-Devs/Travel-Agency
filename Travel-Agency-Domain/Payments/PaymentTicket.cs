namespace Travel_Agency_Domain.Payments;

public class PaymentTicket : Payment
{
    
    public PaymentTicket(Guid userIdentityId, double price) : base(userIdentityId, price)
    {
    }

    public ICollection<ReserveTicket> ReserveTickets { get; set; } = null!;
}