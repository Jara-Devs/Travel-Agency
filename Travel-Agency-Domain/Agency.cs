using Travel_Agency_Core;
using Travel_Agency_Domain.User;

namespace Travel_Agency_Domain;

public class Agency
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public long FaxNumber { get; set; }

    public int AdminId { get; set; }

    public UserAgency Admin { get; set; } = null!;

    public Agency(string name, string email, long faxNumber, int adminId)
    {
        this.Name = name;
        this.Email = email;
        this.FaxNumber = faxNumber;
        this.AdminId = adminId;
    }

    public ICollection<UserAgency> Users { get; set; } = null!;

    public ICollection<UserAgency> Managers => this.Users.Where(u => u.Role == Roles.ManagerAgency).ToList();

    public ICollection<UserAgency> Employees => this.Users.Where(u => u.Role == Roles.EmployeeAgency).ToList();
}