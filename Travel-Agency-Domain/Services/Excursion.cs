using Travel_Agency_Core;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_Domain.Services;

public class Excursion : Entity
{
    public string Name { get; set; }

    public ICollection<TouristPlace> Places { get; set; }

    public ICollection<TouristActivity> Activities { get; set; }

    public Excursion(string name, ICollection<TouristPlace> places, ICollection<TouristActivity> activities)
    {
        this.Name = name;
        this.Places = places;
        this.Activities = activities;
    }


    public ICollection<ExcursionOffer> Offers { get; set; } = null!;
}