using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;

namespace Travel_Agency_Api.Models.User;

[Index(nameof(Id), IsUnique = true)]
public class Employee : User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    [Required] public override string Role { get; set; } = Roles.EmployeeAgency;
}