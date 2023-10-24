namespace Travel_Agency_Core;

public class UserBasic
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public string Role { get; private set; }


    public UserBasic(int id, string name, string role)
    {
        this.Id = id;
        this.Name = name;
        this.Role = role;
    }
}