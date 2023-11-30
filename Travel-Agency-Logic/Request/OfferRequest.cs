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

    public Guid ImageId { get; set; }

    public List<Guid> Facilities { get; set; } = null!;

    public abstract T Offer(Guid agencyId, T? offer = null);
}

public class HotelOfferRequest : OfferRequest<HotelOffer>
{
    public Guid HotelId { get; set; }

    public override HotelOffer Offer(Guid agencyId, HotelOffer? hotelOffer = null)
    {
        hotelOffer ??= new HotelOffer(Name, Availability, Description, Price, StartDate, EndDate, agencyId, HotelId,
            ImageId);
        hotelOffer.Name = Name;
        hotelOffer.Availability = Availability;
        hotelOffer.Description = Description;
        hotelOffer.Price = Price;
        hotelOffer.StartDate = StartDate;
        hotelOffer.EndDate = EndDate;
        hotelOffer.HotelId = HotelId;
        hotelOffer.ImageId = ImageId;

        return hotelOffer;
    }
}

public class ExcursionOfferRequest : OfferRequest<ExcursionOffer>
{
    public Guid ExcursionId { get; set; }

    public override ExcursionOffer Offer(Guid agencyId, ExcursionOffer? excursionOffer = null)
    {
        excursionOffer ??= new ExcursionOffer(Name, Availability, Description, Price, StartDate, EndDate, agencyId,
            ExcursionId, ImageId);
        excursionOffer.Name = Name;
        excursionOffer.Availability = Availability;
        excursionOffer.Description = Description;
        excursionOffer.Price = Price;
        excursionOffer.StartDate = StartDate;
        excursionOffer.EndDate = EndDate;
        excursionOffer.ExcursionId = ExcursionId;
        excursionOffer.ImageId = ImageId;

        return excursionOffer;
    }
}

public class FlightOfferRequest : OfferRequest<FlightOffer>
{
    public Guid FlightId { get; set; }

    public override FlightOffer Offer(Guid agencyId, FlightOffer? flightOffer = null)
    {
        flightOffer ??= new FlightOffer(Name, Availability, Description, Price, StartDate, EndDate, agencyId, FlightId,
            ImageId);
        flightOffer.Name = Name;
        flightOffer.Availability = Availability;
        flightOffer.Description = Description;
        flightOffer.Price = Price;
        flightOffer.StartDate = StartDate;
        flightOffer.EndDate = EndDate;
        flightOffer.FlightId = FlightId;
        flightOffer.ImageId = ImageId;

        return flightOffer;
    }
}