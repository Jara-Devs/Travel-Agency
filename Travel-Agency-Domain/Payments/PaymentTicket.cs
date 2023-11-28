using Travel_Agency_Domain.Others;

namespace Travel_Agency_Domain.Payments;

public class PaymentTicket : Payment
{
    public PaymentTicket()
    {
    }

    public PaymentTicket(UserIdentity userIdentity, double price) : base(userIdentity, price)
    {
    }

    public ICollection<ReserveTicket> ReserveTickets { get; set; } = null!;
}