using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Travel_Agency_Api.Core.Enums;

namespace Travel_Agency_Api.Models;

[Index(nameof(Email), IsUnique = true)]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] public string Name { get; set; } = null!;

    [Required] public string Email { get; set; } = null!;

    [Required] public string Password { get; set; } = null!;

    [Required] public Roles Role { get; set; }
}