using Travel_Agency_Core;
using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Images;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_Domain.Services;

public class Hotel : Entity
{
    public Hotel(string name, HotelCategory category, Guid touristPlaceId, Guid imageId)
    {
        Name = name;
        Category = category;
        TouristPlaceId = touristPlaceId;
        ImageId = imageId;
    }

    public string Name { get; set; }

    public HotelCategory Category { get; set; }

    public Guid TouristPlaceId { get; set; }

    public TouristPlace TouristPlace { get; set; } = null!;

    public Image Image { get; set; } = null!;

    public Guid ImageId { get; set; }

    public ICollection<HotelOffer> Offers { get; set; } = null!;

    public ICollection<Excursion> OverNightExcursions { get; set; } = null!;
}