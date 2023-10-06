using System.ComponentModel.DataAnnotations.Schema;

namespace Travel_Agency_Api.Models.Services;

public class OverNightExcursion : Excursion
{
    [ForeignKey("Hotel")] public int HotelId { get; set; }

    public Hotel Hotel { get; set; } = null!;
}