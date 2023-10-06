using System.ComponentModel.DataAnnotations.Schema;
using Travel_Agency_Api.Models.Services;

namespace Travel_Agency_Api.Models.Offers;

public class HotelOffer:Offer
{
    public HotelOffer(string description, int price,Hotel hotel) : base(description, price)
    {
        this.Hotel = hotel;
    }
    
    [ForeignKey("Hotel")]
    public int HotelId { get; set; }
    
    public Hotel Hotel { get; set; }
}