using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Request;

public class ExcursionRequest
{
    public string Name { get; set; } = null!;

    public Guid ImageId { get; set; }

    public ICollection<Guid> Hotels { get; set; } = null!;

    public ICollection<Guid> Places { get; set; } = null!;

    public ICollection<Guid> Activities { get; set; } = null!;

    public virtual Excursion Excursion(Excursion? excursion = null)
    {
        excursion ??= new Excursion(Name, ImageId);
        excursion.Name = Name;
        excursion.ImageId = ImageId;

        return excursion;
    }
}