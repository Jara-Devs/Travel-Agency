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
        var response = LoginAdmin(request);
        if (response.Ok) return response;

        var user = await this._context.Users.Where(u => u.Email == request.Email).SingleOrDefaultAsync();
        if (user is null) return new NotFound<LoginResponse>("Incorrect email or password");

        if (!SecurityService.CheckPassword(user.Password, request.Password))
            return new BadRequest<LoginResponse>("Incorrect password or password");

        return await LoginResponse(user.Id, user.Name, user.Role);
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

    public async Task<ApiResponse<IdResponse>> RegisterUserAgency(RegisterUserAgencyRequest userAgencyRequest,
        UserBasic user)
    {
        if (user.Role != Roles.AdminAgency)
            return new Unauthorized<IdResponse>("You are not an admin of this agency");

        var admin = await this._context.UserAgencies.FindAsync(user.Id);

        if (userAgencyRequest.Role != Roles.EmployeeAgency && userAgencyRequest.Role != Roles.ManagerAgency)
            return new BadRequest<IdResponse>("Invalid role");

        var check = await CheckRegister(userAgencyRequest.Email, userAgencyRequest.Password);
        if (!check.Ok) return check.ConvertApiResponse<IdResponse>();

        var entity = new UserAgency(userAgencyRequest.Name, userAgencyRequest.Email,
            SecurityService.EncryptPassword(userAgencyRequest.Password), userAgencyRequest.Role,
            admin!.AgencyId);

        this._context.Add(entity);
        await this._context.SaveChangesAsync();

        return new ApiResponse<IdResponse>(new IdResponse { Id = entity.Id });
    }

    public async Task<ApiResponse> RemoveUserAgency(int id, UserBasic userBasic)
    {
        if (userBasic.Role != Roles.AdminAgency)
            return new Unauthorized("You are not an admin of this agency");

        var user = await this._context.UserAgencies.FindAsync(id);
        if (user is null) return new NotFound("Not found user");

        var admin = await this._context.UserAgencies.FindAsync(userBasic.Id);

        if (user.AgencyId != admin!.AgencyId)
            return new Unauthorized("You are not an admin of this agency");

        if (admin.Id == user.Id)
            return new BadRequest("You can't eliminate yourself");

        this._context.Remove(user);
        await this._context.SaveChangesAsync();

        return new ApiResponse();
    }

    public async Task<ApiResponse<IdResponse>> RegisterUserApp(RegisterUserAppRequest userAppRequest,
        UserBasic user)
    {
        if (user.Role != Roles.AdminApp)
            return new Unauthorized<IdResponse>("You are not an admin of app");

        var check = await CheckRegister(userAppRequest.Email, userAppRequest.Password);
        if (!check.Ok) return check.ConvertApiResponse<IdResponse>();

        var entity = new User(userAppRequest.Name, userAppRequest.Email,
            SecurityService.EncryptPassword(userAppRequest.Password), Roles.EmployeeApp);

        this._context.Add(entity);
        await this._context.SaveChangesAsync();

        return new ApiResponse<IdResponse>(new IdResponse { Id = entity.Id });
    }

    public async Task<ApiResponse> RemoveUserApp(int id, UserBasic userBasic)
    {
        if (userBasic.Role != Roles.AdminApp)
            return new Unauthorized("You are not an admin of app");

        var user = await this._context.Users.FindAsync(id);
        if (user is null) return new NotFound("Not found user");

        if (user.Role != Roles.EmployeeApp)
            return new BadRequest("You cannot delete a user who is not from the app");

        this._context.Remove(user);
        await this._context.SaveChangesAsync();

        return new ApiResponse();
    }

    public async Task<ApiResponse<LoginResponse>> Renew(UserBasic user)
    {
        return await LoginResponse(user.Id, user.Name, user.Role);
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

    private ApiResponse<LoginResponse> LoginAdmin(LoginRequest request)
    {
        var (user, password) = this._securityService.AdminCredentials();

        if (user != request.Email || password != request.Password) return new BadRequest<LoginResponse>();

        return new ApiResponse<LoginResponse>(new LoginResponse("Admin",
            this._securityService.JwtAuth(0, "admin", Roles.AdminApp), Roles.AdminApp));
    }

    private async Task<ApiResponse<LoginResponse>> LoginResponse(int id, string name, string role)
    {
        var token = this._securityService.JwtAuth(id, name, role);

        if (role == Roles.Tourist)
        {
            var user = await this._context.Tourists.FindAsync(id);
            return new ApiResponse<LoginResponse>(new LoginResponseTourist(name, token, role, user!.Nationality));
        }

        if (role == Roles.AdminAgency || role == Roles.ManagerAgency || role == Roles.EmployeeAgency)
        {
            var user = await this._context.UserAgencies.Include(x => x.Agency).SingleOrDefaultAsync(x => x.Id == id);
            return new ApiResponse<LoginResponse>(new LoginResponseAgency(name, token, role, user!.AgencyId,
                user!.Agency.Name, user!.Agency.FaxNumber));
        }

        return new ApiResponse<LoginResponse>(new LoginResponse(name,
            this._securityService.JwtAuth(id, name, role), role));
    }
}