using Travel_Agency_Domain.Services;

namespace Travel_Agency_Domain.Offers;

public class HotelOffer : Offer
{
    public int HotelId { get; set; }

    public Hotel Hotel { get; set; } = null!;

    public HotelOffer(string description, double price,int hotelId) : base(description, price)
    {
        this.HotelId = hotelId;
    }
}