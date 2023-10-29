using Travel_Agency_Core;
using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Packages;
using Travel_Agency_Domain.Users;

namespace Travel_Agency_Domain.Payments;

public class PaymentOrder : Entity
{
    public int PackageId { get; set; }

    public Package Package { get; set; } = null!;

    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public double Price { get; set; }

    public PaymentOrderState State { get; set; }

    public PaymentOrder(int packageId, int userId, double price, PaymentOrderState state = PaymentOrderState.Pending)
    {
        this.PackageId = packageId;
        this.UserId = userId;
        this.Price = price;
        this.State = state;
    }
}