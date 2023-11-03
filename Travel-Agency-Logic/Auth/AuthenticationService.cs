using Microsoft.EntityFrameworkCore;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Domain;
using Travel_Agency_Domain.Users;
using Travel_Agency_Logic.Core;

namespace Travel_Agency_Logic.Auth;

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

        return new ApiResponse<LoginResponse>(new LoginResponse(user.Name,
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

    public async Task<ApiResponse<IdResponse>> RegisterManagerAgency(RegisterUserAgencyRequest userAgencyRequest,
        UserBasic user)
    {
        if (user.Role != Roles.AdminAgency)
            return new Unauthorized<IdResponse>("You are not an admin of this agency");

        var check = await CheckRegister(userAgencyRequest.Email, userAgencyRequest.Password);
        if (!check.Ok) return check.ConvertApiResponse<IdResponse>();

        this._context.Add(new UserAgency(userAgencyRequest.Name, userAgencyRequest.Email,
            SecurityService.EncryptPassword(userAgencyRequest.Password), Roles.ManagerAgency,
            userAgencyRequest.AgencyId));
        await this._context.SaveChangesAsync();

        var userAgency =
            await this._context.Users.Where(u => u.Email == userAgencyRequest.Email)
                .Select(x => new IdResponse { Id = x.Id }).SingleOrDefaultAsync();

        return new ApiResponse<IdResponse>(userAgency!);
    }

    public async Task<ApiResponse<IdResponse>> RegisterEmployeeAgency(RegisterUserAgencyRequest userAgencyRequest,
        UserBasic user)
    {
        if (user.Role != Roles.AdminAgency)
            return new Unauthorized<IdResponse>("You are not an admin of this agency");

        var check = await CheckRegister(userAgencyRequest.Email, userAgencyRequest.Password);
        if (!check.Ok) return check.ConvertApiResponse<IdResponse>();

        this._context.Add(new UserAgency(userAgencyRequest.Name, userAgencyRequest.Email,
            SecurityService.EncryptPassword(userAgencyRequest.Password), Roles.EmployeeAgency,
            userAgencyRequest.AgencyId));
        await this._context.SaveChangesAsync();

        var userAgency = await this._context.Users.Where(u => u.Email == userAgencyRequest.Email)
            .Select(x => new IdResponse { Id = x.Id }).SingleOrDefaultAsync();

        return new ApiResponse<IdResponse>(userAgency!);
    }

    public async Task<ApiResponse<IdResponse>> RegisterUserApp(RegisterUserAgencyRequest userAppRequest,
        UserBasic user)
    {
        if (user.Role != Roles.AdminApp)
            return new Unauthorized<IdResponse>("You are not an admin of app");

        var check = await CheckRegister(userAppRequest.Email, userAppRequest.Password);
        if (!check.Ok) return check.ConvertApiResponse<IdResponse>();

        this._context.Add(new User(userAppRequest.Name, userAppRequest.Email,
            SecurityService.EncryptPassword(userAppRequest.Password), Roles.EmployeeApp));
        await this._context.SaveChangesAsync();

        var userApp = await this._context.Users.Where(u => u.Email == userAppRequest.Email)
            .Select(x => new IdResponse { Id = x.Id }).SingleOrDefaultAsync();

        return new ApiResponse<IdResponse>(userApp!);
    }

    public ApiResponse<LoginResponse> Renew(UserBasic user)
    {
        return new ApiResponse<LoginResponse>(new LoginResponse(user.Name, user.Role,
            this._securityService.JwtAuth(user.Id, user.Name, user.Role)));
    }

    public async Task<ApiResponse> ChangePassword(ChangePasswordRequest request, UserBasic userRequest)
    {
        var user = await this._context.Users.FindAsync(userRequest.Id);
        if (user is null) return new NotFound("Not found user");

        if (!SecurityService.CheckPassword(user.Password, request.OldPassword))
            return new BadRequest("Incorrect password");

        user.Password = SecurityService.EncryptPassword(request.NewPassword);

        this._context.Update(user);
        await this._context.SaveChangesAsync();

        return new ApiResponse();
    }

    private async Task<ApiResponse> CheckRegister(string email, string password)
    {
        if (await this._context.Users.CountAsync(u => u.Email == email) != 0)
            return new BadRequest("There is already a user with same email");

        return password.Length <= 5 ? new BadRequest("The password is very short") : new ApiResponse();
    }
}