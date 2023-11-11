using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Offers;
using Travel_Agency_Domain.Packages;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Packages;

public class PackageService : IPackageService
{
    private readonly TravelAgencyContext _context;

    public PackageService(TravelAgencyContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<IdResponse>> CreatePackage(PackageRequest request, UserBasic userBasic)
    {
        var responsePermissions = await CheckPermissions(userBasic);
        if (!responsePermissions.Ok) return responsePermissions.ConvertApiResponse<IdResponse>();

        var response = await CheckRequest(request, responsePermissions.Value);
        if (!response.Ok) return response.ConvertApiResponse<IdResponse>();

        var package = response.Value!;

        _context.Packages.Add(package);
        await _context.SaveChangesAsync();

        return new ApiResponse<IdResponse>(new IdResponse { Id = package.Id });
    }

    public async Task<ApiResponse> UpdatePackage(int id, PackageRequest request, UserBasic userBasic)
    {
        var responsePermissions = await CheckPermissions(userBasic, id);
        if (!responsePermissions.Ok) return responsePermissions.ConvertApiResponse();

        var response = await CheckRequest(request, responsePermissions.Value);
        if (!response.Ok) return response.ConvertApiResponse();

        var package = response.Value!;

        var oldPackage = await _context.Packages.FindAsync(id);
        if (oldPackage is null) return new NotFound("Package not found");

        package.Id = oldPackage.Id;

        _context.Packages.Update(package);
        await _context.SaveChangesAsync();

        return new ApiResponse();
    }

    public async Task<ApiResponse> RemovePackage(int id, UserBasic userBasic)
    {
        var responsePermissions = await CheckPermissions(userBasic, id);
        if (!responsePermissions.Ok) return responsePermissions.ConvertApiResponse();

        var oldPackage = await _context.Packages.FindAsync(id);
        if (oldPackage is null) return new NotFound("Package not found");

        _context.Packages.Remove(oldPackage);
        await _context.SaveChangesAsync();

        return new ApiResponse();
    }

    private async Task<ApiResponse<int>> CheckPermissions(UserBasic userBasic, int id = -1)
    {
        if (userBasic.Role != Roles.AdminAgency || userBasic.Role != Roles.ManagerAgency)
            return new Unauthorized<int>("You don't have permissions");

        var agencyId = await _context.UserAgencies.Where(u => u.Id == userBasic.Id).Select(u => u.AgencyId)
            .FirstOrDefaultAsync();

        if (id == -1) return new ApiResponse<int>(agencyId);

        var package = await _context.Packages.Include(p => p.Offers).Where(p => p.Id == id).FirstOrDefaultAsync();
        if (package is null) return new NotFound<int>("Package not found");

        return package.Offers.Any(o => o.AgencyId != agencyId)
            ? new Unauthorized<int>("You don't have permissions")
            : new ApiResponse<int>(agencyId);
    }

    private async Task<ApiResponse<Package>> CheckRequest(PackageRequest request, int agencyId)
    {
        if (request.Offers.Count == 0) return new BadRequest<Package>("You must add at least one offer");
        if (request.Discount is > 100 or < 0)
            return new BadRequest<Package>("Discount must be between 0 and 100");

        var offers = new List<Offer>();

        foreach (var item in request.Offers)
        {
            var offer = await _context.Offers.FindAsync(item);
            if (offer is null) return new NotFound<Package>("Offer not found");
            if (offer.AgencyId != agencyId) return new Unauthorized<Package>("You don't have permissions");

            if (!Helpers.ValidDate(offer.StartDate)) return new BadRequest<Package>("The offer has expired");
            
            offers.Add(offer);
        }

        var package = request.Package();
        package.Offers = offers;

        return new ApiResponse<Package>(package);
    }
}