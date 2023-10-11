using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_Domain.Services;

public class Hotel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public HotelCategory Category { get; set; }

    public int TouristPlaceId { get; set; }

    public TouristPlace TouristPlace { get; set; } = null!;

    public Hotel(string name, HotelCategory category, int touristPlaceId)
    {
        this.Name = name;
        this.Category = category;
        this.TouristPlaceId = touristPlaceId;
    }

    public ICollection<HotelOffer> Offers { get; set; } = null!;

    public ICollection<OverNightExcursion> OverNightExcursions { get; set; } = null!;
}