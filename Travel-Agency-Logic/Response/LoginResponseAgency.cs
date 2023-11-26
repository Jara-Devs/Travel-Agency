namespace Travel_Agency_Logic.Response;

public class LoginResponseAgency : LoginResponse
{
    public int AgencyId { get; set; }

    public string AgencyName { get; set; }

    public long FaxNumber { get; set; }

    public LoginResponseAgency(int id,string name, string token, string role, int agencyId, string agencyName, long faxNumber)
        : base(id,name, token, role)
    {
        this.AgencyId = agencyId;
        this.AgencyName = agencyName;
        this.FaxNumber = faxNumber;
    }
}