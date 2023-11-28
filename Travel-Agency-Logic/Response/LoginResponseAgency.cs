namespace Travel_Agency_Logic.Response;

public class LoginResponseAgency : LoginResponse
{
    public LoginResponseAgency(Guid id, string name, string token, string role, Guid agencyId, string agencyName,
        long faxNumber)
        : base(id, name, token, role)
    {
        AgencyId = agencyId;
        AgencyName = agencyName;
        FaxNumber = faxNumber;
    }

    public Guid AgencyId { get; set; }

    public string AgencyName { get; set; }

    public long FaxNumber { get; set; }
}