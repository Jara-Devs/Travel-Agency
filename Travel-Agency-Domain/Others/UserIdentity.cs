namespace Travel_Agency_Domain.Others;

public class UserIdentity
{
    public string Name { get; set; }

    public string IdentityDocument { get; set; }

    public UserIdentity(string name, string identityDocument)
    {
        this.Name = name;
        this.IdentityDocument = identityDocument;
    }
}