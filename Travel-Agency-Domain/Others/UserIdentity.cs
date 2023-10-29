namespace Travel_Agency_Domain.Others;

public class UserIdentity
{
    public string Name { get; set; }

    public string IdentityDocument { get; set; }

    public string Address { get; set; }

    public UserIdentity(string name, string identityDocument, string address)
    {
        this.Name = name;
        this.IdentityDocument = identityDocument;
        this.Address = address;
    }
}