using Travel_Agency_Domain;

namespace Travel_Agency_Logic.Response;

public class LoginResponseTourist : LoginResponse
{
    public LoginResponseTourist(Guid id, string name, string token, string role, UserIdentity userIdentity) : base(id,
        name,
        token, role)
    {
        UserIdentity = userIdentity;
    }

    public UserIdentity UserIdentity { get; set; }
}