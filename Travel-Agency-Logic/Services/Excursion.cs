using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Services;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Services;

public class ExcursionService : IExcursionService
{
    private readonly TravelAgencyContext _context;

    public ExcursionService(TravelAgencyContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<IdResponse>> CreateExcursion(ExcursionRequest request, UserBasic user)
    {
        if (!Helpers.CheckPermissions(user))
            return new Unauthorized<IdResponse>("You don't have permissions");

        if (await _context.Excursions.AnyAsync(a => a.Name == request.Name))
            return new NotFound<IdResponse>("The excursion with same name already exists");

        if (request.Places.Count == 0 || request.Activities.Count == 0)
            return new BadRequest<IdResponse>("There must be at least one tourist activity and one tourist place");

        var response = await CreateExcursion(request);
        if (!response.Ok) return response.ConvertApiResponse<IdResponse>();

        var entity = response.Value!;
        _context.Excursions.Add(entity);
        await _context.SaveChangesAsync();

        return new ApiResponse<IdResponse>(new IdResponse { Id = entity.Id });
    }

    public async Task<ApiResponse> UpdateExcursion(Guid id, ExcursionRequest request, UserBasic user)
    {
        if (!Helpers.CheckPermissions(user))
            return new Unauthorized("You don't have permissions");

        var excursion = await _context.Excursions.Include(x => x.Activities).Include(x => x.Places)
            .Include(x => x.Hotels)
            .Where(x => x.Id == id).FirstOrDefaultAsync();
        if (excursion is null) return new NotFound("Excursion not found");

        if (await _context.Excursions.AnyAsync(a => a.Name == request.Name && a.Id != id))
            return new NotFound("The excursion with same name already exists");

        if (request.Places.Count == 0 || request.Activities.Count == 0)
            return new BadRequest("There must be at least one tourist activity and one tourist place");

        var inUse = await CheckDependency(id);
        if (!inUse.Ok) return inUse;

        var response = await CreateExcursion(request, excursion);
        if (!response.Ok) return response.ConvertApiResponse();

        var newExcursion = response.Value!;

        _context.Update(newExcursion);
        await _context.SaveChangesAsync();

        return new ApiResponse();
    }

    public async Task<ApiResponse> DeleteExcursion(Guid id, UserBasic user)
    {
        if (!Helpers.CheckPermissions(user))
            return new Unauthorized("You don't have permissions");

        var inUse = await CheckDependency(id);
        if (!inUse.Ok) return inUse;

        var excursion = await _context.Excursions.FindAsync(id);
        if (excursion is null) return new NotFound("Not found excursion");

        _context.Excursions.Remove(excursion);
        await _context.SaveChangesAsync();

        return new ApiResponse();
    }

    private async Task<ApiResponse<Excursion>> CreateExcursion(ExcursionRequest request,
        Excursion? excursion = null)
    {
        excursion = request.Excursion(excursion);
        excursion.Activities.Clear();
        excursion.Places.Clear();
        excursion.Hotels.Clear();

        foreach (var item in request.Places)
        {
            var place = await _context.TouristPlaces.FindAsync(item);
            if (place is null)
                return new BadRequest<Excursion>("Not found tourist place");

            excursion.Places.Add(place);
        }

        foreach (var item in request.Activities)
        {
            var activity = await _context.TouristActivities.FindAsync(item);
            if (activity is null)
                return new BadRequest<Excursion>("Not found tourist activity");

            excursion.Activities.Add(activity);
        }

        foreach (var item in request.Hotels)
        {
            var hotel = await _context.Hotels.FindAsync(item);
            if (hotel is null)
                return new BadRequest<Excursion>("Not found hotel");

            excursion.Hotels.Add(hotel);
        }

        return new ApiResponse<Excursion>(excursion);
    }

    private async Task<ApiResponse> CheckDependency(Guid id)
    {
        return await _context.ExcursionOffers.AnyAsync(o => o.ExcursionId == id)
            ? new BadRequest("There is an offer for this excursion")
            : new ApiResponse();
    }
}