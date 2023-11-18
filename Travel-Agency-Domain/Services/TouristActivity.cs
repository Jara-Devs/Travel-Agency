using Travel_Agency_Core;

namespace Travel_Agency_Domain.Services;

public class TouristActivity : Entity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public Image Image { get; set; } = null!;  

    public int ImageId { get; set; }

    public TouristActivity(string name, string description, int imageId)
    {
        this.Name = name;
        this.Description = description;
        this.ImageId = imageId;
    }

    public ICollection<Excursion> Excursions { get; set; } = null!;
}