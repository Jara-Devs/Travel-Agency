using System.ComponentModel.DataAnnotations.Schema;
using Travel_Agency_Api.Models.Services;

namespace Travel_Agency_Api.Models.Offers;

public class ExcursionOffer : Offer
{
    [ForeignKey("Excursion")] public int ExcursionId { get; set; }

    public Excursion Excursion { get; set; } = null!;
}