using Travel_Agency_Domain.Services;

namespace Travel_Agency_Domain.Offers;

public class HotelOffer : Offer
{
    public int HotelId { get; set; }

    public Hotel Hotel { get; set; } = null!;

    public HotelOffer(string name, int availability, string description, double price, long startDate, long endDate, int hotelId) 
        : base(description, price, name, availability, startDate, endDate) {
        this.HotelId = hotelId;
    }
}