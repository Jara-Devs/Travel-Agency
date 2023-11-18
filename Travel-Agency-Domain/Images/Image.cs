using Travel_Agency_Core;

namespace Travel_Agency_Domain.Images;

public class Image : Entity
{
    public string Url { get; set; }

    public string Name { get; set; }

    public Image(string name, string url)
    {
        this.Url = url;
        this.Name = name;
    }
}