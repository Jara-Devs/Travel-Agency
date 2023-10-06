using System.ComponentModel.DataAnnotations.Schema;
using Travel_Agency_Api.Models.Services;

namespace Travel_Agency_Api.Models.Offers;

public class FlightOffer : Offer
{
    public FlightOffer(string description, int price, Flight flight) : base(description, price)
    {
        this.Flight = flight;
    }

    [ForeignKey("Flight")] public int FlightId { get; set; }

    public Flight Flight { get; set; }
}