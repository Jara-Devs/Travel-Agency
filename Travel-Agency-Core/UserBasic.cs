namespace Travel_Agency_Core;

public class UserBasic
{
    public UserBasic(Guid id, string name, string role)
    {
        Id = id;
        Name = name;
        Role = role;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Role { get; private set; }
}