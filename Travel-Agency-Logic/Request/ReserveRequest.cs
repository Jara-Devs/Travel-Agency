using Travel_Agency_Domain.Others;
using Travel_Agency_Domain.Payments;

namespace Travel_Agency_Logic.Request;

public abstract class ReserveRequest<T1, T2> where T1 : Reserve where T2 : Payment
{
    public Guid Id { get; set; }

    public bool IsSingleOffer { get; set; }
    public ICollection<UserIdentity> UserIdentities { get; set; } = null!;

    public UserIdentity UserIdentity { get; set; } = null!;
    public abstract T2 Payment(double price);

    public abstract T1 Reserve(Guid packageId, Guid paymentId, Guid userId);
}

public class ReserveTouristRequest : ReserveRequest<ReserveTourist, PaymentOnline>
{
    public long CreditCard { get; set; }

    public override PaymentOnline Payment(double price)
    {
        return new PaymentOnline(UserIdentity, price, CreditCard);
    }

    public override ReserveTourist Reserve(Guid packageId, Guid paymentId, Guid userId)
    {
        return new ReserveTourist(packageId, UserIdentities, userId, paymentId);
    }
}

public class ReserveTicketRequest : ReserveRequest<ReserveTicket, PaymentTicket>
{
    public override PaymentTicket Payment(double price)
    {
        return new PaymentTicket(UserIdentity, price);
    }

    public override ReserveTicket Reserve(Guid packageId, Guid paymentId, Guid userId)
    {
        return new ReserveTicket(packageId, UserIdentities, userId, paymentId);
    }
}