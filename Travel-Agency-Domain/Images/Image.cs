using Travel_Agency_Core;

namespace Travel_Agency_Domain.Images;

public class Image : Entity
{
    public Image(string name, string url)
    {
        Url = url;
        Name = name;
    }

    public string Url { get; set; }

    public string Name { get; set; }
}