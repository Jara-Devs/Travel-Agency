using Travel_Agency_Domain.Others;
using Travel_Agency_Domain.Payments;

namespace Travel_Agency_Logic.Request;

public abstract class ReserveRequest<T1, T2> where T1 : Reserve where T2 : Payment
{
    public Guid UserId { get; set; }

    public Guid PackageId { get; set; }

    public ICollection<UserIdentity> UserIdentities { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string IdentityDocument { get; set; } = null!;

    public abstract T2 Payment(double price);

    public abstract T1 Reserve(Guid paymentId);
}

public class ReserveTouristRequest : ReserveRequest<ReserveTourist, PaymentOnline>
{
    public int CreditCard { get; set; }

    public override PaymentOnline Payment(double price)
    {
        return new PaymentOnline(new UserIdentity(Name, IdentityDocument), price, CreditCard);
    }

    public override ReserveTourist Reserve(Guid paymentId)
    {
        return new ReserveTourist(PackageId, UserIdentities, UserId, paymentId);
    }
}

public class ReserveTicketRequest : ReserveRequest<ReserveTicket, PaymentTicket>
{
    public override PaymentTicket Payment(double price)
    {
        return new PaymentTicket(new UserIdentity(Name, IdentityDocument), price);
    }

    public override ReserveTicket Reserve(Guid paymentId)
    {
        return new ReserveTicket(PackageId, UserIdentities, UserId, paymentId);
    }
}