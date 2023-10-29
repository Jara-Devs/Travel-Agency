using Travel_Agency_Core;
using Travel_Agency_Domain.Users;

namespace Travel_Agency_Domain;

public class Agency : Entity
{
    public string Name { get; set; }

    public long FaxNumber { get; set; }

    public Agency(string name, long faxNumber)
    {
        this.Name = name;
        this.FaxNumber = faxNumber;
    }

    public ICollection<UserAgency> Users { get; set; } = null!;

    public UserAgency Admin() => this.Users.First(u => u.Role == Roles.AdminAgency);

    public ICollection<UserAgency> Managers() => this.Users.Where(u => u.Role == Roles.ManagerAgency).ToList();

    public ICollection<UserAgency> Employees() => this.Users.Where(u => u.Role == Roles.EmployeeAgency).ToList();
}