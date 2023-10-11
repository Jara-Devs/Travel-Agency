namespace Travel_Agency_Domain.Services;

public class OverNightExcursion : Excursion
{
   public int HotelId { get; set; }

    public Hotel Hotel { get; set; } = null!;

    public OverNightExcursion(string name, ICollection<TouristPlace> places, ICollection<TouristActivity> activities,
        int hotelId) : base(name, places, activities)
    {
        this.HotelId = hotelId;
    }
}