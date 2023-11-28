namespace Travel_Agency_Logic.Response;

public class LoginResponse
{
    public LoginResponse(Guid id, string name, string token, string role)
    {
        Id = id;
        Role = role;
        Name = name;
        Token = token;
    }

    public Guid Id { get; }
    public string Name { get; }

    public string Token { get; }

    public string Role { get; }
}