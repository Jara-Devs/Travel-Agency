using Travel_Agency_Core;

namespace Travel_Agency_Domain.Services;

public class TouristActivity : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public TouristActivity(string name, string description)
    {
        this.Name = name;
        this.Description = description;
    }

    public ICollection<Excursion> Excursions { get; set; } = null!;
}