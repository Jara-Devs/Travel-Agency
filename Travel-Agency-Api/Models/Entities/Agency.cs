using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Travel_Agency_Api.Models.User;

namespace Travel_Agency_Api.Models.Entities;

[Index(nameof(Id), IsUnique = true)]
public class Agency
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] public string Name { get; set; } = null!;

    [Required] public string Email { get; set; } = null!;

    [Required] public int FaxNumber { get; set; }

    [ForeignKey("Admin")] public int AdminId { get; set; }

    public Admin Admin { get; set; } = null!;

    public ICollection<Employee> Employees { get; set; } = null!;

    public ICollection<Manager> Managers { get; set; } = null!;
}