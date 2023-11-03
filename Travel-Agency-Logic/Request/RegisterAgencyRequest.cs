namespace Travel_Agency_Logic.Request;

public class RegisterAgencyRequest
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string NameAgency { get; set; } = null!;

    public long FaxNumber { get; set; }
}