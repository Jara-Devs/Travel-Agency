using Travel_Agency_Api.Core;
using Travel_Agency_Api.DataBase;
using Travel_Agency_Api.Request;
using Travel_Agency_Api.Response;

namespace Travel_Agency_Api.Service;

public interface IAuthenticationService
{
    Task<ApiResponse<LoginResponse>> Login(LoginRequest login);

    Task<ApiResponse<LoginResponse>> Register(RegisterRequest request);

    ApiResponse<string> Renew(string token);
}

public class AuthenticationService
{
    private readonly TravelAgencyContext _context;

    public AuthenticationService(TravelAgencyContext context)
    {
        this._context = context;
    }

    public async Task<ApiResponse<LoginResponse>> Login(LoginRequest login)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<LoginResponse>> Register(RegisterRequest request)
    {
        throw new NotImplementedException();
    }

    public ApiResponse<string> Renew(string token)
    {
        throw new NotImplementedException();
    }
}