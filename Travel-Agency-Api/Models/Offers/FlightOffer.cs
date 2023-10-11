using System.ComponentModel.DataAnnotations.Schema;
using Travel_Agency_Api.Models.Services;

namespace Travel_Agency_Api.Models.Offers;

public class FlightOffer : Offer
{
    [ForeignKey("Flight")] public int FlightId { get; set; }

    public Flight Flight { get; set; } = null!;
}