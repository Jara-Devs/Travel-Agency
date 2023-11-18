using Travel_Agency_Core;
using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_Domain.Services;

public class Hotel : Entity
{
    public string Name { get; set; }

    public HotelCategory Category { get; set; }

    public int TouristPlaceId { get; set; }

    public TouristPlace TouristPlace { get; set; } = null!;

    public Image Image { get; set; } = null!;

    public int ImageId { get; set; }

    public Hotel(string name, HotelCategory category, int touristPlaceId, int imageId)
    {
        this.Name = name;
        this.Category = category;
        this.TouristPlaceId = touristPlaceId;
        this.ImageId = imageId;
    }

    public ICollection<HotelOffer> Offers { get; set; } = null!;

    public ICollection<OverNightExcursion> OverNightExcursions { get; set; } = null!;
}