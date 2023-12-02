using Travel_Agency_Core;
using Travel_Agency_Domain.Payments;

namespace Travel_Agency_Domain;

public class UserIdentity : Entity

{
    public UserIdentity(string name, string identityDocument, string nationality)
    {
        Name = name;
        IdentityDocument = identityDocument;
        Nationality = nationality;
    }

    public string Name { get; set; }
    public string IdentityDocument { get; set; }

    public string Nationality { get; set; }

    public ICollection<Reserve> Reserves { get; set; } = null!;
}