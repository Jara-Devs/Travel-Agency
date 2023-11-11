using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Request;

public class ExcursionRequest
{
    public string Name { get; set; } = null!;

    public ICollection<int> Places { get; set; } = null!;

    public ICollection<int> Activities { get; set; } = null!;

    public virtual Excursion Excursion() => new Excursion(this.Name);
}