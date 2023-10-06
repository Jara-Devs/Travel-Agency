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

    [Required] public string Company { get; set; }

    [Required] public FlightCategory FlightCategory { get; set; }

    [Required] public long Duration { get; set; }

    [ForeignKey("TouristPlace")] public int Place1Id { get; set; }
    public TouristPlace Place1 { get; set; }

    [ForeignKey("TouristPlace")] public int Place2Id { get; set; }
    public TouristPlace Place2 { get; set; }

    public Flight(string company, long duration, FlightCategory flightCategory, TouristPlace place1,
        TouristPlace place2)
    {
        this.Company = company;
        this.Place1 = place1;
        this.Place2 = place2;
        this.FlightCategory = flightCategory;
    }
    
    public ICollection<FlightOffer> Offers { get; set; } = null!;
}