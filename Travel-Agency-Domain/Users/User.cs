using Travel_Agency_Core;

namespace Travel_Agency_Domain.Users;

public class User : Entity
{
    public User(string name, string email, string password, string role)
    {
        Name = name;
        Email = email;
        Password = password;
        Role = role;
    }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string Role { get; set; }
}