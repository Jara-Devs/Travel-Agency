using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Services;

public class TouristActivityService : ITouristActivityService
{
    private readonly TravelAgencyContext _context;

    public TouristActivityService(TravelAgencyContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<IdResponse>> CreateTouristActivity(TouristActivityRequest touristActivity,
        UserBasic user)
    {
        if (!Helpers.CheckPermissions(user))
            return new Unauthorized<IdResponse>("You don't have permissions");

        if (await _context.TouristActivities.AnyAsync(ta => ta.Name == touristActivity.Name))
            return new NotFound<IdResponse>("The tourist activity with same name already exists");

        var entity = touristActivity.TouristActivity();
        _context.TouristActivities.Add(entity);
        await _context.SaveChangesAsync();

        return new ApiResponse<IdResponse>(new IdResponse { Id = entity.Id });
    }

    public async Task<ApiResponse> UpdateTouristActivity(Guid id, TouristActivityRequest touristActivityRequest,
        UserBasic user)
    {
        if (!Helpers.CheckPermissions(user))
            return new Unauthorized("You don't have permissions");

        var touristActivity = await _context.TouristActivities.FindAsync(id);
        if (touristActivity is null) return new NotFound("Tourist activity not found");

        var inUse = await CheckDependency(id);
        if (!inUse.Ok) return inUse;

        if (await _context.TouristActivities.AnyAsync(ta => ta.Name == touristActivityRequest.Name && id != ta.Id))
            return new NotFound("The tourist activity with same name already exists");

        var newTouristActivity = touristActivityRequest.TouristActivity(touristActivity);
        newTouristActivity.Id = id;

        _context.Update(newTouristActivity);
        await _context.SaveChangesAsync();

        return new ApiResponse();
    }

    public async Task<ApiResponse> DeleteTouristActivity(Guid id, UserBasic user)
    {
        if (!Helpers.CheckPermissions(user))
            return new Unauthorized("You don't have permissions");

        var inUse = await CheckDependency(id);
        if (!inUse.Ok) return inUse;

        var touristActivity = await _context.TouristActivities.FindAsync(id);
        if (touristActivity is null) return new NotFound("Tourist activity not found");

        _context.TouristActivities.Remove(touristActivity);
        await _context.SaveChangesAsync();

        return new ApiResponse();
    }

    private async Task<ApiResponse> CheckDependency(Guid id)
    {
        return await _context.Excursions.Include(e => e.Activities).AnyAsync(e => e.Activities.Any(a => a.Id == id))
            ? new BadRequest("There is an excursion for this activity")
            : new ApiResponse();
    }
}