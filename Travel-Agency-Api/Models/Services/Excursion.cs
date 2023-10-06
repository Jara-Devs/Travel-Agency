using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Travel_Agency_Api.Models.Offers;

namespace Travel_Agency_Api.Models.Services;

[Index(nameof(Name), IsUnique = true)]
public class Excursion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] public string Name { get; set; } = null!;

    public ICollection<TouristPlace> Places { get; set; } = null!;

    public ICollection<TouristActivity> Activities { get; set; } = null!;

    public ICollection<ExcursionOffer> Offers { get; set; } = null!;
}