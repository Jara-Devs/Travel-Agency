using Travel_Agency_Domain.Others;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Request;

public class TouristPlaceRequest
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public Guid CityId { get; set; }

    public string Address { get; set; } = null!;

    public Guid ImageId { get; set; }

    public TouristPlace TouristPlace(TouristPlace? touristPlace = null)
    {
        touristPlace ??= new TouristPlace(Name, Description, Address, CityId, ImageId);
        touristPlace.Name = Name;
        touristPlace.Description = Description;
        touristPlace.Address = Address;
        touristPlace.CityId = CityId;
        touristPlace.ImageId = ImageId;

        return touristPlace;
    }
}