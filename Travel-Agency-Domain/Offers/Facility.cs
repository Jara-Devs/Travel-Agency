using Travel_Agency_Core;
using Travel_Agency_Core.Enums;

namespace Travel_Agency_Domain.Offers;

public class Facility : Entity
{
    public string Name { get; set; }

    public FacilityType Type { get; set; }

    public Facility(string name, FacilityType type)
    {
        this.Name = name;
        this.Type = type;
    }
    
    public ICollection<Offer> Offers { get; set; } = null!;
}