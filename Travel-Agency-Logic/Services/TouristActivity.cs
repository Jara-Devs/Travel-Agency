using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Services
{
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
            if (!CheckPermissions(user))
                return new Unauthorized<IdResponse>("You don't have permissions");

            if (await _context.TouristActivities.AnyAsync(ta => ta.Name == touristActivity.Name))
                return new NotFound<IdResponse>("The tourist activity already exists");

            _context.TouristActivities.Add(touristActivity.TouristActivity());
            await _context.SaveChangesAsync();

            return new ApiResponse<IdResponse>((await this._context.TouristActivities
                .Where(x => x.Name == touristActivity.Name).Select(x => new IdResponse { Id = x.Id })
                .SingleOrDefaultAsync())!);
        }

        public async Task<ApiResponse> UpdateTouristActivity(int id, TouristActivityRequest touristActivity, UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized("You don't have permissions");

            if (!await _context.TouristActivities.AnyAsync(ta => ta.Id == id))
                return new NotFound("Tourist activity not found");

            var newTouristActivity = touristActivity.TouristActivity();
            newTouristActivity.Id = id;

            _context.Update(newTouristActivity);
            await _context.SaveChangesAsync();

            return new ApiResponse();
        }

        public async Task<ApiResponse> DeleteTouristActivity(int id, UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized("You don't have permissions");

            var touristActivity = await _context.TouristActivities.FindAsync(id);
            if (touristActivity is null) return new NotFound("Tourist activity not found");

            _context.TouristActivities.Remove(touristActivity!);
            await _context.SaveChangesAsync();

            return new ApiResponse();
        }

        private static bool CheckPermissions(UserBasic user) =>
            user.Role == Roles.AdminApp || user.Role == Roles.EmployeeApp;
    }
}