using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Domain;
using Travel_Agency_Domain.Users;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Auth;

public class AuthenticationService : IAuthenticationService
{
    private readonly TravelAgencyContext _context;

    private readonly SecurityService _securityService;

    public AuthenticationService(TravelAgencyContext context, SecurityService securityService)
    {
        _context = context;
        _securityService = securityService;
    }

    public async Task<ApiResponse<LoginResponse>> Login(LoginRequest request)
    {
        var response = LoginAdmin(request);
        if (response.Ok) return response;

        var user = await _context.Users.Where(u => u.Email == request.Email).SingleOrDefaultAsync();
        if (user is null) return new NotFound<LoginResponse>("Incorrect email or password");

        if (!SecurityService.CheckPassword(user.Password, request.Password))
            return new BadRequest<LoginResponse>("Incorrect password or password");

        return await LoginResponse(user.Id, user.Name, user.Role);
    }

    public async Task<ApiResponse<LoginResponse>> RegisterTourist(RegisterTouristRequest touristRequest)
    {
        var check = await CheckRegister(touristRequest.Email, touristRequest.Password);
        if (!check.Ok) return check.ConvertApiResponse<LoginResponse>();

        var identity = await Helpers.ManageUserIdentity(new[] { touristRequest.UserIdentity }, _context);

        _context.Add(new Tourist(touristRequest.Name, touristRequest.Email,
            SecurityService.EncryptPassword(touristRequest.Password), identity[0].Id));

        await _context.SaveChangesAsync();

        var user = await _context.Users.Where(u => u.Email == touristRequest.Email).SingleOrDefaultAsync();

        return await LoginResponse(user!.Id, user.Name, user.Role);
    }

    public async Task<ApiResponse<LoginResponse>> RegisterAgency(RegisterAgencyRequest agencyRequest)
    {
        if (await _context.Agencies.CountAsync(a => a.Name == agencyRequest.NameAgency) != 0)
            return new BadRequest<LoginResponse>("There is already an agency with same name");

        var check = await CheckRegister(agencyRequest.Email, agencyRequest.Password);
        if (!check.Ok) return check.ConvertApiResponse<LoginResponse>();

        _context.Add(new Agency(agencyRequest.NameAgency, agencyRequest.FaxNumber, agencyRequest.Address));
        await _context.SaveChangesAsync();

        var agency = await _context.Agencies.Where(a => a.Name == agencyRequest.NameAgency).SingleOrDefaultAsync();

        _context.Add(new UserAgency(agencyRequest.Name, agencyRequest.Email,
            SecurityService.EncryptPassword(agencyRequest.Password), Roles.AdminAgency, agency!.Id));
        await _context.SaveChangesAsync();

        var user = await _context.Users.Where(u => u.Email == agencyRequest.Email).SingleOrDefaultAsync();

        return await LoginResponse(user!.Id, user.Name, user.Role);
    }

    public async Task<ApiResponse<IdResponse>> RegisterUserAgency(RegisterUserAgencyRequest userAgencyRequest,
        UserBasic user)
    {
        if (user.Role != Roles.AdminAgency)
            return new Unauthorized<IdResponse>("You are not an admin of this agency");

        var admin = await _context.UserAgencies.FindAsync(user.Id);

        if (userAgencyRequest.Role != Roles.EmployeeAgency && userAgencyRequest.Role != Roles.ManagerAgency)
            return new BadRequest<IdResponse>("Invalid role");

        var check = await CheckRegister(userAgencyRequest.Email, userAgencyRequest.Password);
        if (!check.Ok) return check.ConvertApiResponse<IdResponse>();

        var entity = new UserAgency(userAgencyRequest.Name, userAgencyRequest.Email,
            SecurityService.EncryptPassword(userAgencyRequest.Password), userAgencyRequest.Role,
            admin!.AgencyId);

        _context.Add(entity);
        await _context.SaveChangesAsync();

        return new ApiResponse<IdResponse>(new IdResponse { Id = entity.Id });
    }

    public async Task<ApiResponse> RemoveUserAgency(Guid id, UserBasic userBasic)
    {
        if (userBasic.Role != Roles.AdminAgency)
            return new Unauthorized("You are not an admin of this agency");

        var user = await _context.UserAgencies.FindAsync(id);
        if (user is null) return new NotFound("Not found user");

        var admin = await _context.UserAgencies.FindAsync(userBasic.Id);

        if (user.AgencyId != admin!.AgencyId)
            return new Unauthorized("You are not an admin of this agency");

        if (admin.Id == user.Id)
            return new BadRequest("You can't eliminate yourself");

        _context.Remove(user);
        await _context.SaveChangesAsync();

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

        _context.Add(entity);
        await _context.SaveChangesAsync();

        return new ApiResponse<IdResponse>(new IdResponse { Id = entity.Id });
    }

    public async Task<ApiResponse> RemoveUserApp(Guid id, UserBasic userBasic)
    {
        if (userBasic.Role != Roles.AdminApp)
            return new Unauthorized("You are not an admin of app");

        var user = await _context.Users.FindAsync(id);
        if (user is null) return new NotFound("Not found user");

        if (user.Role != Roles.EmployeeApp)
            return new BadRequest("You cannot delete a user who is not from the app");

        _context.Remove(user);
        await _context.SaveChangesAsync();

        return new ApiResponse();
    }

    public async Task<ApiResponse<LoginResponse>> Renew(UserBasic user)
    {
        return await LoginResponse(user.Id, user.Name, user.Role);
    }

    public async Task<ApiResponse> ChangePassword(ChangePasswordRequest request, UserBasic userRequest)
    {
        var user = await _context.Users.FindAsync(userRequest.Id);
        if (user is null) return new NotFound("Not found user");

        if (!SecurityService.CheckPassword(user.Password, request.OldPassword))
            return new BadRequest("Incorrect password");

        user.Password = SecurityService.EncryptPassword(request.NewPassword);

        _context.Update(user);
        await _context.SaveChangesAsync();

        return new ApiResponse();
    }

    private async Task<ApiResponse> CheckRegister(string email, string password)
    {
        if (await _context.Users.CountAsync(u => u.Email == email) != 0)
            return new BadRequest("There is already a user with same email");

        return password.Length <= 5 ? new BadRequest("The password is too short") : new ApiResponse();
    }

    private ApiResponse<LoginResponse> LoginAdmin(LoginRequest request)
    {
        var (user, password) = _securityService.AdminCredentials();

        if (user != request.Email || password != request.Password) return new BadRequest<LoginResponse>();

        return new ApiResponse<LoginResponse>(new LoginResponse(Guid.NewGuid(), "Admin",
            _securityService.JwtAuth(Guid.NewGuid(), "admin", Roles.AdminApp), Roles.AdminApp));
    }

    private async Task<ApiResponse<LoginResponse>> LoginResponse(Guid id, string name, string role)
    {
        var token = _securityService.JwtAuth(id, name, role);

        if (role == Roles.Tourist)
        {
            var user = await _context.Tourists.Include(x => x.UserIdentity).SingleOrDefaultAsync(x => x.Id == id);
            return new ApiResponse<LoginResponse>(new LoginResponseTourist(id, name, token, role, user!.UserIdentity));
        }

        if (role == Roles.AdminAgency || role == Roles.ManagerAgency || role == Roles.EmployeeAgency)
        {
            var user = await _context.UserAgencies.Include(x => x.Agency).SingleOrDefaultAsync(x => x.Id == id);
            return new ApiResponse<LoginResponse>(new LoginResponseAgency(id, name, token, role, user!.AgencyId,
                user!.Agency.Name, user!.Agency.FaxNumber));
        }

        return new ApiResponse<LoginResponse>(new LoginResponse(id, name,
            _securityService.JwtAuth(id, name, role), role));
    }
}