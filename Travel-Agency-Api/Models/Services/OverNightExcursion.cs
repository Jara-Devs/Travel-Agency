using System.ComponentModel.DataAnnotations.Schema;

namespace Travel_Agency_Api.Models.Services;

public class OverNightExcursion : Excursion
{
    public OverNightExcursion(string name, ICollection<TouristPlace> places, ICollection<TouristActivity> activities,Hotel hotel) :
        base(name, places, activities)
    {
        this.Hotel = hotel;
    }
    
    [ForeignKey("Hotel")]
    public int HotelId { get; set; }
    
    public Hotel Hotel { get; set; }
}