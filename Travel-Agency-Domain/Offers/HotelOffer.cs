using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Packages;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Domain.Offers;

public class HotelOffer : Offer
{
    public HotelOffer(string name, int availability, string description, double price, long startDate,
        long endDate, Guid agencyId, Guid hotelId, Guid imageId)
        : base(description, price, name, availability, startDate, endDate, agencyId, imageId, OfferType.Hotel)
    {
        HotelId = hotelId;
    }

    public Guid HotelId { get; set; }

    public Hotel Hotel { get; set; } = null!;
    
    public ICollection<Package> Packages { get; set; } = null!;
}