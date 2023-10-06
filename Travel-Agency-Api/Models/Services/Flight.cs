using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Travel_Agency_Api.Core.Enums;
using Travel_Agency_Api.Models.Offers;

namespace Travel_Agency_Api.Models.Services;

public class Flight
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] public string Company { get; set; } = null!;

    [Required] public FlightCategory FlightCategory { get; set; }

    [Required] public long Duration { get; set; }

    [ForeignKey("TouristPlace")] public int Place1Id { get; set; }
    public TouristPlace Place1 { get; set; } = null!;

    [ForeignKey("TouristPlace")] public int Place2Id { get; set; }
    public TouristPlace Place2 { get; set; } = null!;

    public ICollection<FlightOffer> Offers { get; set; } = null!;
}