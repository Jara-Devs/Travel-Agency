using Travel_Agency_Core;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_Domain.Services;

public class Excursion : Entity
{
    public string Name { get; set; }

    public ICollection<TouristPlace> Places { get; set; } = null!;

    public ICollection<TouristActivity> Activities { get; set; } = null!;

    public Excursion(string name)
    {
        this.Name = name;
    }


    public ICollection<ExcursionOffer> Offers { get; set; } = null!;
}