using Travel_Agency_Core;

namespace Travel_Agency_Domain.Payments;

public class Payment : Entity
{
    public int OrderId { get; set; }

    public PaymentOrder Order { get; set; } = null!;
}