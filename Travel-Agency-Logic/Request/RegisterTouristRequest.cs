namespace Travel_Agency_Logic.Request;

public class RegisterTouristRequest
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public UserIdentityRequest UserIdentity { get; set; } = null!;
}