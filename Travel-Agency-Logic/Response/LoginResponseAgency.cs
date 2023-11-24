namespace Travel_Agency_Logic.Response;

public class LoginResponseAgency : LoginResponse
{
    public int AgencyId { get; set; }

    public string AgencyName { get; set; }

    public long FaxNumber { get; set; }

    public LoginResponseAgency(string name, string token, string role, int agencyId, string agencyName, long faxNumber)
        : base(name, token, role)
    {
        this.AgencyId = agencyId;
        this.AgencyName = agencyName;
        this.FaxNumber = faxNumber;
    }
}