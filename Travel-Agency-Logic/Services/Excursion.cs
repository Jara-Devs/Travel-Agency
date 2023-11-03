using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Services;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Services
{
    public class ExcursionService : IExcursionService
    {
        private readonly TravelAgencyContext _context;

        public ExcursionService(TravelAgencyContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IdResponse>> CreateExcursion(ExcursionRequest request, UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized<IdResponse>("You don't have permissions");

            if (!await _context.Excursions.AnyAsync(a => a.Name == request.Name))
                return new NotFound<IdResponse>("The excursion already exists");

            if (request.Places.Count == 0 || request.Activities.Count == 0)
                return new BadRequest<IdResponse>("There must be at least one tourist activity and one tourist place");

            var response = await CreateExcursion(request);
            if (!response.Ok) return response.ConvertApiResponse<IdResponse>();

            _context.Excursions.Add(response.Value!);
            await _context.SaveChangesAsync();

            return new ApiResponse<IdResponse>((await this._context.Excursions.Where(x => x.Name == request.Name)
                .Select(x => new IdResponse { Id = x.Id })
                .SingleOrDefaultAsync())!);
        }

        public async Task<ApiResponse> UpdateExcursion(int id, ExcursionRequest request, UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized("You don't have permissions");

            var excursion = await this._context.Excursions.FindAsync(id);
            if (excursion is null) return new NotFound("Not found excursion");

            if (request.Places.Count == 0 || request.Activities.Count == 0)
                return new BadRequest("There must be at least one tourist activity and one tourist place");

            var response = await CreateExcursion(request);
            if (!response.Ok) return response.ConvertApiResponse();

            var newExcursion = response.Value!;
            newExcursion.Id = excursion.Id;

            _context.Update(newExcursion);
            await _context.SaveChangesAsync();

            return new ApiResponse();
        }

        public async Task<ApiResponse> DeleteExcursion(int id, UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized("You don't have permissions");

            var excursion = await _context.Excursions.FindAsync(id);
            if (excursion is null) return new NotFound("Excursion not found");

            _context.Excursions.Remove(excursion);
            await _context.SaveChangesAsync();

            return new ApiResponse();
        }

        private static bool CheckPermissions(UserBasic user) =>
            user.Role == Roles.AdminApp || user.Role == Roles.EmployeeApp;

        private async Task<ApiResponse<Excursion>> CreateExcursion(ExcursionRequest request)
        {
            var excursion = request.Excursion();
            foreach (var item in request.Places)
            {
                var place = await this._context.TouristPlaces.FindAsync(item);
                if (place is null)
                    return new BadRequest<Excursion>("Not found touristic place");

                excursion.Places.Add(place);
            }

            foreach (var item in request.Activities)
            {
                var activity = await this._context.TouristActivities.FindAsync(item);
                if (activity is null)
                    return new BadRequest<Excursion>("Not found touristic place");

                excursion.Activities.Add(activity);
            }

            return new ApiResponse<Excursion>(excursion);
        }
    }
}