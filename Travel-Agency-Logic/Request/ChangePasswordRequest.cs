namespace Travel_Agency_Logic.Request;

public class ChangePasswordRequest
{
    public string OldPassword { get; set; } = null!;

    public string NewPassword { get; set; } = null!;
}