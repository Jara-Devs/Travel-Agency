using Travel_Agency_Api.Core.Enums;

namespace Travel_Agency_Api.Response;

public class LoginResponse
{
    public string Name { get; }

    public string Token { get; }

    public Roles Role { get; }

    public LoginResponse(string name, string token, Roles role)
    {
        this.Role = role;
        this.Name = name;
        this.Token = token;
    }
}