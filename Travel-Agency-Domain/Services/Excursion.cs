using Travel_Agency_Core;
using Travel_Agency_Domain.Images;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_Domain.Services;

public class Excursion : Entity
{
    public string Name { get; set; }

    public bool IsOverNight { get; set; }

    public Guid? HotelId { get; set; }

    public Hotel? Hotel { get; set; }

    public ICollection<TouristPlace> Places { get; set; }

    public ICollection<TouristActivity> Activities { get; set; }

    public Image Image { get; set; } = null!;

    public Guid ImageId { get; set; }

    public Excursion(string name, Guid imageId, Guid? hotelId = null)
    {
        this.Name = name;
        this.HotelId = hotelId;
        this.IsOverNight = hotelId != null;
        this.ImageId = imageId;
        this.Activities = new List<TouristActivity>();
        this.Places = new List<TouristPlace>();
    }

    public ICollection<ExcursionOffer> Offers { get; set; } = null!;
}