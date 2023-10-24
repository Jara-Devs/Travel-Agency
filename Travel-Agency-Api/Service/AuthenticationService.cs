using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
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

    private readonly SecurityService _securityService;

    public AuthenticationService(TravelAgencyContext context, SecurityService securityService)
    {
        this._context = context;
        this._securityService = securityService;
    }

    public async Task<ApiResponse<LoginResponse>> Login(LoginRequest request)
    {
        var user = await this._context.Users.Where(u => u.Email == request.Email).SingleOrDefaultAsync();
        if (user is null) return new NotFound<LoginResponse>("Incorrect email");

        if (!SecurityService.CheckPassword(request.Password, user.Password))
            return new BadRequest<LoginResponse>("Incorrect password");

        return new ApiResponse<LoginResponse>(new LoginResponse(user.Name, user.Role,
            this._securityService.JwtAuth(user.Id, user.Name, user.Role)));
    }

    public async Task<ApiResponse<LoginResponse>> Register(RegisterRequest request)
    {
        throw new NotImplementedException();
    }

    public ApiResponse<LoginResponse> Renew(UserBasic user)
    {
        return new ApiResponse<LoginResponse>(new LoginResponse(user.Name, user.Role,
            this._securityService.JwtAuth(user.Id, user.Name, user.Role)));
    }
}