using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Request;

public class ExcursionRequest
{
    public string Name { get; set; } = null!;

    public int ImageId { get; set; }

    public ICollection<int> Places { get; set; } = null!;

    public ICollection<int> Activities { get; set; } = null!;

    public virtual Excursion Excursion(Excursion? excursion = null)
    {
        excursion ??= new (this.Name, this.ImageId, false);
        excursion.Name = this.Name;
        excursion.ImageId = this.ImageId;
       
        return excursion;
    }
}