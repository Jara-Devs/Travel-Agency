using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Travel_Agency_Api.Core.Enums;
using Travel_Agency_Api.Models.Offers;

namespace Travel_Agency_Api.Models.Services;

[Index(nameof(Name), IsUnique = true)]
public class Hotel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] public string Name { get; set; }

    [Required] public HotelCategory Category { get; set; }

    [ForeignKey("TouristPlace")]
    [Required]
    public int TouristPlaceId { get; set; }

    public TouristPlace TouristPlace { get; set; }

    public Hotel(string name, HotelCategory category, TouristPlace touristPlace)
    {
        this.Name = name;
        this.TouristPlace = touristPlace;
        this.Category = category;
    }

    public ICollection<HotelOffer> Offers { get; set; } = null!;

    public ICollection<OverNightExcursion> OverNightExcursions { get; set; } = null!;
}