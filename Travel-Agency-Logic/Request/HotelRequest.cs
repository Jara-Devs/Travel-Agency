using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Request;

public class HotelRequest
{
    public string Name { get; set; } = null!;

    public HotelCategory Category { get; set; }

    public Guid TouristPlaceId { get; set; }

    public Guid ImageId { get; set; }

    public Hotel Hotel(Hotel? hotel = null)
    {
        hotel ??= new Hotel(Name, Category, TouristPlaceId, ImageId);
        hotel.Name = Name;
        hotel.Category = Category;
        hotel.TouristPlaceId = TouristPlaceId;
        hotel.ImageId = ImageId;

        return hotel;
    }
}