namespace Travel_Agency_Core;

public class UserBasic
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Role { get; private set; }


    public UserBasic(Guid id, string name, string role)
    {
        this.Id = id;
        this.Name = name;
        this.Role = role;
    }
}