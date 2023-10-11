using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;

namespace Travel_Agency_Api.Models.User;

[Index(nameof(Id), IsUnique = true)]
public class Tourist : User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    [Required] public string Nacionality { get; set; } = null!;

    [Required] public override string Role { get; set; } = Roles.Tourist;
}