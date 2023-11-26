namespace Travel_Agency_Logic.Response;

public class LoginResponseAgency : LoginResponse
{
    public Guid AgencyId { get; set; }

    public string AgencyName { get; set; }

    public long FaxNumber { get; set; }

    public LoginResponseAgency(Guid id,string name, string token, string role, Guid agencyId, string agencyName, long faxNumber)
        : base(id,name, token, role)
    {
        this.AgencyId = agencyId;
        this.AgencyName = agencyName;
        this.FaxNumber = faxNumber;
    }
}