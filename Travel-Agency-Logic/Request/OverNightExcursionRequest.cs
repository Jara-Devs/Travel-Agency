using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Request;

public class OverNightExcursionRequest : ExcursionRequest
{
    public int HotelId { get; set; }

    public OverNightExcursion Excursion(OverNightExcursion? excursion = null)
    {
        excursion ??= new OverNightExcursion(this.Name, this.HotelId, this.ImageId);
        excursion.Name = this.Name;
        excursion.HotelId = this.HotelId;
        excursion.ImageId = this.ImageId;

        return excursion;
    }
}