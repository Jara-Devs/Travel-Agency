namespace Travel_Agency_Api.Request;

public class RegisterTouristRequest
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Nationality { get; set; } = null!;
}