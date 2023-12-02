using Travel_Agency_Domain;

namespace Travel_Agency_Logic.Request;

public class UserIdentityRequest
{
    public string Name { get; set; } = null!;

    public long IdentityDocument { get; set; }

    public string Nationality { get; set; } = null!;

    public UserIdentity UserIdentity() => new(Name, IdentityDocument, Nationality);
}