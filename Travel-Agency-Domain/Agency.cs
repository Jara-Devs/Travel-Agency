using Travel_Agency_Core;
using Travel_Agency_Domain.Users;

namespace Travel_Agency_Domain;

public class Agency : Entity
{
    public Agency(string name, long faxNumber)
    {
        Name = name;
        FaxNumber = faxNumber;
    }

    public string Name { get; set; }

    public long FaxNumber { get; set; }

    public ICollection<UserAgency> Users { get; set; } = null!;

    public UserAgency Admin()
    {
        return Users.First(u => u.Role == Roles.AdminAgency);
    }

    public ICollection<UserAgency> Managers()
    {
        return Users.Where(u => u.Role == Roles.ManagerAgency).ToList();
    }

    public ICollection<UserAgency> Employees()
    {
        return Users.Where(u => u.Role == Roles.EmployeeAgency).ToList();
    }
}