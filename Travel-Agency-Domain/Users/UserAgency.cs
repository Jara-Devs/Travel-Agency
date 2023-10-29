namespace Travel_Agency_Domain.Users;

public class UserAgency : User
{
    public int AgencyId { get; set; }

    public Agency Agency { get; set; } = null!;

    public UserAgency(string name, string email, string password, string role, int agencyId) : base(name, email,
        password, role)
    {
        this.AgencyId = agencyId;
    }
}