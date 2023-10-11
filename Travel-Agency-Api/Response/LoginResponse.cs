namespace Travel_Agency_Api.Response;

public class LoginResponse
{
    public string Name { get; }

    public string Token { get; }

    public string Role { get; }

    public LoginResponse(string name, string token, string role)
    {
        this.Role = role;
        this.Name = name;
        this.Token = token;
    }
}