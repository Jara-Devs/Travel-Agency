using Travel_Agency_Domain.Others;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Request;

public class TouristActivityRequest
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int ImageId { get; set; }

    public TouristActivity TouristActivity() => new(this.Name, this.Description, this.ImageId);

}