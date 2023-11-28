using Travel_Agency_Core;
using Travel_Agency_Domain.Images;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_Domain.Services;

public class Excursion : Entity
{
    public string Name { get; set; }

    public ICollection<Hotel> Hotels { get; set; }

    public ICollection<TouristPlace> Places { get; set; }

    public ICollection<TouristActivity> Activities { get; set; }

    public Image Image { get; set; } = null!;

    public Guid ImageId { get; set; }

    public Excursion(string name, Guid imageId)
    {
        this.Name = name;
        this.ImageId = imageId;
        this.Hotels = new List<Hotel>();
        this.Activities = new List<TouristActivity>();
        this.Places = new List<TouristPlace>();
    }

    public ICollection<ExcursionOffer> Offers { get; set; } = null!;
}