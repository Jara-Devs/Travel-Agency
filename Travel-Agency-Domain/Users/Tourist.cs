using Travel_Agency_Core;
using Travel_Agency_Domain.Payments;
using Travel_Agency_Domain.Reactions;

namespace Travel_Agency_Domain.Users;

public class Tourist : User
{
    public Tourist(string name, string email, string password, string nationality) : base(name, email, password,
        Roles.Tourist)
    {
        Nationality = nationality;
    }

    public string Nationality { get; set; }

    public ICollection<ReserveTourist> Reserves { get; set; } = null!;

    public ICollection<Reaction> Reactions { get; set; } = null!;
}