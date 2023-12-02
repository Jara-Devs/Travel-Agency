using Travel_Agency_Domain;
using Travel_Agency_Domain.Payments;

namespace Travel_Agency_Logic.Request;

public abstract class ReserveRequest<T1, T2> where T1 : Reserve where T2 : Payment
{
    public Guid Id { get; set; }

    public bool IsSingleOffer { get; set; }
    public ICollection<UserIdentityRequest> UserIdentities { get; set; } = null!;

    public UserIdentityRequest UserIdentity { get; set; } = null!;
    public abstract T2 Payment(Guid userIdentityId, double price);

    public abstract T1 Reserve(Guid packageId, Guid paymentId, Guid userId, int cant);
}

public class ReserveTouristRequest : ReserveRequest<ReserveTourist, PaymentOnline>
{
    public long CreditCard { get; set; }

    public override PaymentOnline Payment(Guid userIdentity, double price)
    {
        return new PaymentOnline(userIdentity, price, CreditCard);
    }

    public override ReserveTourist Reserve(Guid packageId, Guid paymentId, Guid userId, int cant)
    {
        return new ReserveTourist(packageId, userId, paymentId, cant);
    }
}

public class ReserveTicketRequest : ReserveRequest<ReserveTicket, PaymentTicket>
{
    public override PaymentTicket Payment(Guid userIdentityId, double price)
    {
        return new PaymentTicket(userIdentityId, price);
    }

    public override ReserveTicket Reserve(Guid packageId, Guid paymentId, Guid userId, int cant)
    {
        return new ReserveTicket(packageId, userId, paymentId, cant);
    }
}