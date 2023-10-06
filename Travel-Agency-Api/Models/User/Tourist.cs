using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Travel_Agency_Api.Core.Enums;

namespace Travel_Agency_Api.Models.User;

[Index(nameof(Id), IsUnique = true)]
public class Tourist : User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    [Required] public string Nacionality { get; set; } = null!;

    [Required] public override Roles Role { get; set; } = Roles.User;
}