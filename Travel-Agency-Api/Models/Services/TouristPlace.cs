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

    [Required] public string Name { get; set; }

    [Required] public string Description { get; set; }

    [Required] public Address Address { get; set; }

    public TouristPlace(string name, string description, Address address)
    {
        this.Name = name;
        this.Description = description;
        this.Address = address;
    }

    public ICollection<Excursion> Excursions { get; set; } = null!;

    public ICollection<Hotel> Hotels { get; set; } = null!;
}