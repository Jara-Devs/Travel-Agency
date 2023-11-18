using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_Logic.Request;

public abstract class OfferRequest<T> where T : Offer
{
    public string Name { get; set; } = null!;

    public int Availability { get; set; } = 0;

    public int AgencyId { get; set; } = 0;

    public string Description { get; set; } = null!;

    public double Price { get; set; } = 0;

    public long StartDate { get; set; } = 0;

    public long EndDate { get; set; } = 0;

    public int ImageId { get; set; } = 0;

    public abstract T Offer();
}

public class HotelOfferRequest : OfferRequest<HotelOffer>
{
    public int HotelId { get; set; } = 0;

    public List<HotelFacility> Facilities { get; set; } = null!;

    public override HotelOffer Offer() 
        => new(Name, Availability, Description, Price, StartDate, EndDate, AgencyId, HotelId, Facilities, ImageId);
}

public class ExcursionOfferRequest : OfferRequest<ExcursionOffer>
{
    public int ExcursionId { get; set; } = 0;

    public List<ExcursionFacility> Facilities { get; set; } = null!;

    public override ExcursionOffer Offer() 
        => new(Name, Availability, Description, Price, StartDate, EndDate, AgencyId, ExcursionId, Facilities, ImageId);
}

public class FlightOfferRequest : OfferRequest<FlightOffer>
{
    public int FlightId { get; set; } = 0;

    public List<FlightFacility> Facilities { get; set; } = null!;

    public override FlightOffer Offer() 
        => new(Name, Availability, Description, Price, StartDate, EndDate, AgencyId, FlightId, Facilities, ImageId);
}