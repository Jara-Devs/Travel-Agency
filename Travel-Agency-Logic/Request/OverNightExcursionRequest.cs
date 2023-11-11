using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Request;

public class OverNightExcursionRequest : ExcursionRequest 
{
    public int HotelId;

    public override OverNightExcursion Excursion() => new(this.Name, this.HotelId);
}