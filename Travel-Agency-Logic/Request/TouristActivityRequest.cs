using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Request;

public class TouristActivityRequest
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public Guid ImageId { get; set; }

    public TouristActivity TouristActivity(TouristActivity? touristActivity = null)
    {
        touristActivity ??= new TouristActivity(Name, Description, ImageId);
        touristActivity.Name = Name;
        touristActivity.Description = Description;
        touristActivity.ImageId = ImageId;

        return touristActivity;
    }
}