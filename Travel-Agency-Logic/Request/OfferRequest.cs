using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_Logic.Request;

public abstract class OfferRequest<T> where T : Offer
{
    public string Name { get; set; } = null!;

    public int Availability { get; set; } = 0;
    
    public string Description { get; set; } = null!;

    public double Price { get; set; } = 0;

    public long StartDate { get; set; } = 0;

    public long EndDate { get; set; } = 0;

    public int ImageId { get; set; } = 0;

    public abstract T Offer(int agencyId,T? offer = null);
}

public class HotelOfferRequest : OfferRequest<HotelOffer>
{
    public int HotelId { get; set; } = 0;

    public List<HotelFacility> Facilities { get; set; } = null!;
    
    public override HotelOffer Offer(int agencyId,HotelOffer? hotelOffer = null)
    {
        hotelOffer ??= new(Name, Availability, Description, Price, StartDate, EndDate, agencyId, HotelId, Facilities, ImageId);
        hotelOffer.Name = this.Name;
        hotelOffer.Availability = this.Availability;
        hotelOffer.Description = this.Description;
        hotelOffer.Price = this.Price;
        hotelOffer.StartDate = this.StartDate;
        hotelOffer.EndDate = this.EndDate;
        hotelOffer.HotelId = this.HotelId;
        hotelOffer.Facilities = this.Facilities;
        hotelOffer.ImageId = this.ImageId;

        return hotelOffer;
    }
}

public class ExcursionOfferRequest : OfferRequest<ExcursionOffer>
{
    public int ExcursionId { get; set; } = 0;

    public List<ExcursionFacility> Facilities { get; set; } = null!;

    public override ExcursionOffer Offer(int agencyId,ExcursionOffer? excursionOffer = null) 
    {
        excursionOffer ??= new(Name, Availability, Description, Price, StartDate, EndDate, agencyId, ExcursionId, Facilities, ImageId);
        excursionOffer.Name = this.Name;
        excursionOffer.Availability = this.Availability;
        excursionOffer.Description = this.Description;
        excursionOffer.Price = this.Price;
        excursionOffer.StartDate = this.StartDate;
        excursionOffer.EndDate = this.EndDate;
        excursionOffer.ExcursionId = this.ExcursionId;
        excursionOffer.Facilities = this.Facilities;
        excursionOffer.ImageId = this.ImageId;

        return excursionOffer;
    }
}

public class FlightOfferRequest : OfferRequest<FlightOffer>
{
    public int FlightId { get; set; } = 0;

    public List<FlightFacility> Facilities { get; set; } = null!;

    public override FlightOffer Offer(int agencyId,FlightOffer? flightOffer = null)
    {
        flightOffer ??= new(Name, Availability, Description, Price, StartDate, EndDate, agencyId, FlightId, Facilities, ImageId);
        flightOffer.Name = this.Name;
        flightOffer.Availability = this.Availability;
        flightOffer.Description = this.Description;
        flightOffer.Price = this.Price;
        flightOffer.StartDate = this.StartDate;
        flightOffer.EndDate = this.EndDate;
        flightOffer.FlightId = this.FlightId;
        flightOffer.Facilities = this.Facilities;
        flightOffer.ImageId = this.ImageId;

        return flightOffer;
    }

}