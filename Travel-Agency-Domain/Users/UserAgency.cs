using System.Collections;
using Travel_Agency_Domain.Payments;

namespace Travel_Agency_Domain.Users;

public class UserAgency : User
{
    public Guid AgencyId { get; set; }

    public Agency Agency { get; set; } = null!;

    public UserAgency(string name, string email, string password, string role, Guid agencyId) : base(name, email,
        password, role)
    {
        this.AgencyId = agencyId;
    }

    public ICollection<ReserveTicket> Reserves { get; set; } = null!;
}