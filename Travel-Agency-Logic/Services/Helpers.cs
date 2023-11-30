using Travel_Agency_Core;

namespace Travel_Agency_Logic.Services;

public static class Helpers
{
    public static bool CheckPermissions(UserBasic user)
    {
        return user.Role == Roles.AdminApp || user.Role == Roles.EmployeeApp;
    }
}