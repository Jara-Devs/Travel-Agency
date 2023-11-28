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
        excursion ??= new(this.Name, this.ImageId);
        excursion.Name = this.Name;
        excursion.ImageId = this.ImageId;

        return excursion;
    }
}