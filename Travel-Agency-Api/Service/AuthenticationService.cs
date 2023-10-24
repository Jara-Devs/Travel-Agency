using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Api.Request;
using Travel_Agency_Api.Response;
using Travel_Agency_Domain;
using Travel_Agency_Domain.User;

namespace Travel_Agency_Api.Service;

public interface IAuthenticationService
{
    Task<ApiResponse<LoginResponse>> Login(LoginRequest login);

    Task<ApiResponse<LoginResponse>> RegisterTourist(RegisterTouristRequest touristRequest);

    Task<ApiResponse<LoginResponse>> RegisterAgency(RegisterAgencyRequest agencyRequest);

    ApiResponse<LoginResponse> Renew(UserBasic user);
}

public class AuthenticationService : IAuthenticationService
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

        if (!SecurityService.CheckPassword(user.Password, request.Password))
            return new BadRequest<LoginResponse>("Incorrect password");

        return new ApiResponse<LoginResponse>(new LoginResponse(user!.Name,
            this._securityService.JwtAuth(user.Id, user.Name, user.Role), user.Role));
    }

    public async Task<ApiResponse<LoginResponse>> RegisterTourist(RegisterTouristRequest touristRequest)
    {
        var check = await CheckRegister(touristRequest.Email, touristRequest.Password);
        if (!check.Ok) return check.ConvertApiResponse<LoginResponse>();

        this._context.Add(new Tourist(touristRequest.Name, touristRequest.Email,
            SecurityService.EncryptPassword(touristRequest.Password), touristRequest.Nationality));

        await this._context.SaveChangesAsync();

        var user = await this._context.Users.Where(u => u.Email == touristRequest.Email).SingleOrDefaultAsync();

        return new ApiResponse<LoginResponse>(new LoginResponse(user!.Name,
            this._securityService.JwtAuth(user.Id, user.Name, user.Role), user.Role));
    }

    public async Task<ApiResponse<LoginResponse>> RegisterAgency(RegisterAgencyRequest agencyRequest)
    {
        var check = await CheckRegister(agencyRequest.Email, agencyRequest.Password);
        if (!check.Ok) return check.ConvertApiResponse<LoginResponse>();

        this._context.Add(new Agency(agencyRequest.NameAgency, agencyRequest.FaxNumber));
        await this._context.SaveChangesAsync();

        var agency = await this._context.Agencies.Where(a => a.Name == agencyRequest.NameAgency).SingleOrDefaultAsync();

        this._context.Add(new UserAgency(agencyRequest.Name, agencyRequest.Email,
            SecurityService.EncryptPassword(agencyRequest.Password), Roles.AdminAgency, agency!.Id));
        await this._context.SaveChangesAsync();

        var user = await this._context.Users.Where(u => u.Email == agencyRequest.Email).SingleOrDefaultAsync();

        return new ApiResponse<LoginResponse>(new LoginResponse(user!.Name,
            this._securityService.JwtAuth(user.Id, user.Name, user.Role), user.Role));
    }

    public ApiResponse<LoginResponse> Renew(UserBasic user)
    {
        return new ApiResponse<LoginResponse>(new LoginResponse(user.Name, user.Role,
            this._securityService.JwtAuth(user.Id, user.Name, user.Role)));
    }

    private async Task<ApiResponse> CheckRegister(string email, string password)
    {
        if (await this._context.Users.CountAsync(u => u.Email == email) != 0)
            return new BadRequest("There is already a user with same email");

        return password.Length <= 5 ? new BadRequest("The password is very short") : new ApiResponse();
    }
}