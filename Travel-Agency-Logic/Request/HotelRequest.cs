using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Request;

public class HotelRequest
{
    public string Name { get; set; } = null!;

    public HotelCategory Category { get; set; }

    public int TouristPlaceId { get; set; }

    public Hotel Hotel(Hotel? hotel = null) 
    {
        hotel ??= new Hotel(this.Name, this.Category, this.TouristPlaceId);
        hotel.Name = this.Name;
        hotel.Category = this.Category;
        hotel.TouristPlaceId = this.TouristPlaceId;

        return hotel;
    }
}