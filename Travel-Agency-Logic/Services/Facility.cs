using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Services;

public class Facility : IFacilityService
{
    private readonly TravelAgencyContext _context;

    public Facility(TravelAgencyContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<IdResponse>> CreateFacility(FacilityRequest request, UserBasic user)
    {
        if (!CheckPermissions(user))
            return new Unauthorized<IdResponse>("You don't have permissions");

        if (await _context.Facilities.AnyAsync(a => a.Name == request.Name))
            return new NotFound<IdResponse>("The facility with same name already exists");

        var entity = request.Facility();
        _context.Facilities.Add(entity);
        await _context.SaveChangesAsync();

        return new ApiResponse<IdResponse>(new IdResponse { Id = entity.Id });
    }

    public async Task<ApiResponse> UpdateFacility(Guid id, FacilityRequest request, UserBasic user)
    {
        if (!CheckPermissions(user))
            return new Unauthorized("You don't have permissions");

        var facility = await _context.Facilities.Include(x => x.Offers).Where(x => x.Id == id).FirstOrDefaultAsync();
        if (facility is null) return new NotFound("Facility not found");

        if (facility.Offers.Any())
            return new BadRequest("The facility is in use");

        if (await _context.Facilities.AnyAsync(a => a.Name == request.Name && a.Id != id))
            return new NotFound("The facility with same name already exists");

        var newFacility = request.Facility(facility);

        _context.Update(newFacility);
        await _context.SaveChangesAsync();

        return new ApiResponse();
    }

    public async Task<ApiResponse> DeleteFacility(Guid id, UserBasic user)
    {
        if (!CheckPermissions(user))
            return new Unauthorized("You don't have permissions");

        var facility = await _context.Facilities.Include(x => x.Offers).Where(x => x.Id == id).FirstOrDefaultAsync();
        if (facility is null) return new NotFound("Facility not found");

        if (facility.Offers.Any())
            return new BadRequest("The facility is in use");

        _context.Facilities.Remove(facility);
        await _context.SaveChangesAsync();

        return new ApiResponse();
    }

    private static bool CheckPermissions(UserBasic user)
    {
        return user.Role == Roles.AdminApp || user.Role == Roles.EmployeeApp;
    }
}