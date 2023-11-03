namespace Travel_Agency_Logic.Request;

public class RegisterUserAppRequest
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;
}