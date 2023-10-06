using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Travel_Agency_Api.Models.Services;

[Index(nameof(Name), IsUnique = true)]
public class TouristActivity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }

    public TouristActivity(string name, string description)
    {
        this.Name = name;
        this.Description = description;
    }

    public ICollection<Excursion> Excursions { get; set; } = null!;
}