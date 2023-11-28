namespace Travel_Agency_Domain.Others;

public class UserIdentity
{
    public UserIdentity(string name, string identityDocument)
    {
        Name = name;
        IdentityDocument = identityDocument;
    }

    public string Name { get; set; }

    public string IdentityDocument { get; set; }
}