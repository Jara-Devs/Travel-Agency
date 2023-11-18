namespace Travel_Agency_Domain.Services;

public class OverNightExcursion : Excursion
{
    public int HotelId { get; set; }

    public Hotel Hotel { get; set; } = null!;

    public OverNightExcursion(string name, int hotelId, int imageId) : base(name, imageId, true)
    {
        this.HotelId = hotelId;
    }
}