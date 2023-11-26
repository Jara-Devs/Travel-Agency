namespace Travel_Agency_Logic.Response;

public class LoginResponseTourist : LoginResponse
{
    public string Nationality { get; set; }

    public LoginResponseTourist(int id, string name, string token, string role, string nationality) : base(id, name,
        token, role)
    {
        this.Nationality = nationality;
    }
}