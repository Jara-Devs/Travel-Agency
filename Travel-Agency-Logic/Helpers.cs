namespace Travel_Agency_Logic;

public static class Helpers
{
    public static bool ValidDate(long date)
    {
        var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(date);
        var dateTime = dateTimeOffset.DateTime;

        return DateTime.UtcNow <= dateTime;
    }
}