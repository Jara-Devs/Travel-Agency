using Travel_Agency_Core;

public class Image : Entity
{
    public string Url { get; set; }

    public Image(string url) {
        this.Url = url;
    }
}