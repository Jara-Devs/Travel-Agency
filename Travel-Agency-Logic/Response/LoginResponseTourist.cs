namespace Travel_Agency_Logic.Response;

public class LoginResponseTourist : LoginResponse
{
    public LoginResponseTourist(Guid id, string name, string token, string role, string nationality) : base(id, name,
        token, role)
    {
        Nationality = nationality;
    }

    public string Nationality { get; set; }
}