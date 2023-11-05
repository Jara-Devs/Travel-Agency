using Travel_Agency_Domain.Others;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Request;

public class TouristPlaceRequest
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public Address Address { get; set; } = null!;

    public TouristPlace TouristPlace() => new TouristPlace(this.Name, this.Description, this.Address);

}