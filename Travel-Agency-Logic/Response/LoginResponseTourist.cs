namespace Travel_Agency_Logic.Response;

public class LoginResponseTourist : LoginResponse
{
    public string Nationality { get; set; } 

    public LoginResponseTourist(string name, string token, string role,string nationality):base(name,token,role)
    {
        this.Nationality = nationality;
    }
}