using Travel_Agency_Domain.Others;
using Travel_Agency_Domain.Payments;

namespace Travel_Agency_Logic.Request;

public abstract class ReserveRequest<T1, T2> where T1 : Reserve where T2 : Payment
{
    public int UserId { get; set; }

    public int PackageId { get; set; }

    public ICollection<UserIdentity> UserIdentities { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string IdentityDocument { get; set; } = null!;

    public abstract T2 Payment(double price);

    public abstract T1 Reserve(int paymentId);
}

public class ReserveTouristRequest : ReserveRequest<ReserveTourist, PaymentOnline>
{
    public int CreditCard { get; set; }

    public override PaymentOnline Payment(double price) => new (new(Name, IdentityDocument), price, CreditCard);

    public override ReserveTourist Reserve(int paymentId) => new (PackageId, UserIdentities, UserId, paymentId);
}

public class ReserveTicketRequest : ReserveRequest<ReserveTicket, PaymentTicket>
{
    public override PaymentTicket Payment(double price)=> new (new(Name, IdentityDocument), price);

    public override ReserveTicket Reserve(int paymentId) => new (PackageId, UserIdentities, UserId, paymentId);
}