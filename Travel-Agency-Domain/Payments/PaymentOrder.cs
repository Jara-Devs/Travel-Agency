using Travel_Agency_Core;
using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Packages;

namespace Travel_Agency_Domain.Payments;

public class PaymentOrder : Entity
{
    public double Price { get; set; }

    public PaymentOrderState State { get; set; }

    public PaymentOrder(double price, PaymentOrderState state = PaymentOrderState.Pending)
    {
        this.Price = price;
        this.State = state;
    }

    public ICollection<Reserve> Reserves { get; set; } = null!;
}