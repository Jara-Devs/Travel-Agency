namespace Travel_Agency_Logic;

public static class Helpers
{
    public static bool ValidDate(long seconds)
    {
        var date = new DateTime(1970, 1, 1).AddSeconds(seconds);
        return date > DateTime.Now;
    }
}