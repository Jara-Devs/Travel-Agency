using Travel_Agency_Core;
using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_Domain.Services;

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