namespace Travel_Agency_Logic.Request;

public class LoginRequest
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}