using Travel_Agency_Core;

namespace Travel_Agency_Domain.Payments;

public class Payment : Entity
{
    public Payment(Guid userIdentityId, double price)
    {
        UserIdentityId = userIdentityId;
        Price = price;
    }

    public double Price { get; set; }

    public Guid UserIdentityId { get; set; }
    public UserIdentity UserIdentity { get; set; } = null!;
}