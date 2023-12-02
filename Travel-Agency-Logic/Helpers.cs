using Microsoft.EntityFrameworkCore;
using Travel_Agency_DataBase;
using Travel_Agency_Domain;
using Travel_Agency_Logic.Request;

namespace Travel_Agency_Logic;

public static class Helpers
{
    public static bool ValidDate(long date)
    {
        var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(date);
        var dateTime = dateTimeOffset.DateTime;

        return DateTime.UtcNow <= dateTime;
    }

    public static async Task<List<UserIdentity>> ManageUserIdentity(IEnumerable<UserIdentityRequest> userIdentities,
        TravelAgencyContext context)
    {
        var users = new List<UserIdentity>();

        foreach (var userIdentity in userIdentities)
        {
            var userIdentityDb =
                await context.UserIdentities.FirstOrDefaultAsync(u =>
                    u.IdentityDocument == userIdentity.IdentityDocument);

            if (userIdentityDb is null)
            {
                var newUser = userIdentity.UserIdentity();
                context.UserIdentities.Add(newUser);
                users.Add(newUser);
            }
            else users.Add(userIdentityDb);
        }

        await context.SaveChangesAsync();

        return users;
    }
}