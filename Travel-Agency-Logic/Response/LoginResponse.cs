namespace Travel_Agency_Logic.Response;

public class LoginResponse
{
    public Guid Id { get; }
    public string Name { get; }

    public string Token { get; }

    public string Role { get; }

    public LoginResponse(Guid id, string name, string token, string role)
    {
        this.Id = id;
        this.Role = role;
        this.Name = name;
        this.Token = token;
    }
}