using Travel_Agency_Domain.Others;
using Travel_Agency_Domain.Users;

namespace Travel_Agency_Domain.Payments;

public class PaymentTicket : Payment
{
    public int UserAgencyId { get; set; }

    public UserAgency UserAgency { get; set; } = null!;

    public PaymentTicket(int orderId, UserIdentity userIdentity, int userAgencyId) : base(orderId, userIdentity)
    {
        this.UserAgencyId = userAgencyId;
    }
}