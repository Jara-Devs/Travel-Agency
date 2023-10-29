using Travel_Agency_Core;
using Travel_Agency_Domain.Others;

namespace Travel_Agency_Domain.Payments;

public class Payment : Entity
{
    public double Price { get; set; }
    public UserIdentity UserIdentity { get; set; }

    public Payment(UserIdentity userIdentity,double price)
    {
        this.UserIdentity = userIdentity;
    }
}