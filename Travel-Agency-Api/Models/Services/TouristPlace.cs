using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Travel_Agency_Api.Models.Entities;

namespace Travel_Agency_Api.Models.Services;

[Index(nameof(Name), IsUnique = true)]
public class TouristPlace
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] public string Name { get; set; } = null!;

    [Required] public string Description { get; set; } = null!;

    [Required] public Address Address { get; set; } = null!;

    public ICollection<Excursion> Excursions { get; set; } = null!;

    public ICollection<Hotel> Hotels { get; set; } = null!;
}