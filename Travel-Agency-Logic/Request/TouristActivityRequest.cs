using Travel_Agency_Domain.Others;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Request;

public class TouristActivityRequest
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public Guid ImageId { get; set; }

    public TouristActivity TouristActivity(TouristActivity? touristActivity = null)
    {
        touristActivity ??= new TouristActivity(this.Name, this.Description, this.ImageId);
        touristActivity.Name = this.Name;
        touristActivity.Description = this.Description;
        touristActivity.ImageId = this.ImageId;

        return touristActivity;
    }
}