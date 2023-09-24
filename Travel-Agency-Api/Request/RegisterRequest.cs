namespace Travel_Agency_Api.Request;

public class RegisterRequest
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;
}