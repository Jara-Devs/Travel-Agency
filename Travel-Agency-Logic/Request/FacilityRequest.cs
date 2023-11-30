using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Request;

public class FacilityRequest
{
    public string Name { get; set; } = null!;

    public FacilityType Type { get; set; }

    public Facility Facility(Facility? facility = null)
    {
        facility ??= new Facility(this.Name, this.Type);
        facility.Name = this.Name;
        facility.Type = this.Type;

        return facility;
    }
}