using Travel_Agency_Core;
using Travel_Agency_Domain.Others;
using Travel_Agency_Domain.Packages;
using Travel_Agency_Domain.Users;

namespace Travel_Agency_Domain.Payments;

public class Reserve : Entity
{
    public int PackageId { get; set; }

    public Package Package { get; set; } = null!;
    
    public int PaymentOrderId { get; set; }

    // public PaymentOrder PaymentOrder { get; set; } = null!;

    public ICollection<UserIdentity> UserIdentities { get; set; }

    public Reserve(int packageId, ICollection<UserIdentity> userIdentities,int paymentOrderId)
    {
        this.PackageId = packageId;
        this.UserIdentities = userIdentities;
        this.PaymentOrderId = paymentOrderId;
    }
}