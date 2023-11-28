using Travel_Agency_Core;
using Travel_Agency_Domain.Images;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_Domain.Services;

public class Excursion : Entity
{
    public Excursion(string name, Guid imageId)
    {
        Name = name;
        ImageId = imageId;
        Hotels = new List<Hotel>();
        Activities = new List<TouristActivity>();
        Places = new List<TouristPlace>();
    }

    public string Name { get; set; }

    public ICollection<Hotel> Hotels { get; set; }

    public ICollection<TouristPlace> Places { get; set; }

    public ICollection<TouristActivity> Activities { get; set; }

    public Image Image { get; set; } = null!;

    public Guid ImageId { get; set; }

    public ICollection<ExcursionOffer> Offers { get; set; } = null!;
}