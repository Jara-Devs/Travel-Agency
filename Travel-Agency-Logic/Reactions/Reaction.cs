using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Reactions
{
    public class ReactionService : IReactionService
    {
        private readonly TravelAgencyContext _context;

        public ReactionService(TravelAgencyContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IdResponse>> CreateReaction(ReactionRequest reaction, UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized<IdResponse>("You don't have permissions");

            var exists = await _context.Reactions
                .AnyAsync(r => r.OfferId == reaction.OfferId && r.TouristId == reaction.TouristId);
            if (exists)
                return new NotFound<IdResponse>("Reaction already exists");

            var offer = await _context.Offers.FindAsync(reaction.OfferId);
            if (offer is null)
                return new NotFound<IdResponse>("Offer not found");
            
            var tourist = await _context.Tourists.FindAsync(reaction.TouristId);
            if (tourist is null)
                return new NotFound<IdResponse>("Tourist not found");

            var entity = reaction.Reaction();
            _context.Reactions.Add(entity);
            await _context.SaveChangesAsync();

            return new ApiResponse<IdResponse>(new IdResponse { Id = entity.Id });
        }

        public async Task<ApiResponse> UpdateReaction(int id, ReactionRequest reaction, UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized("You don't have permissions");

            var exists = await _context.Reactions
                .AnyAsync(r => r.OfferId == reaction.OfferId && r.TouristId == reaction.TouristId); 
            if (!exists)
                return new NotFound("Reaction not found");

            var offer = await _context.Offers.FindAsync(reaction.OfferId);
            if (offer is null)
                return new NotFound("Offer not found");

            var tourist = await _context.Tourists.FindAsync(reaction.TouristId);
            if (tourist is null)
                return new NotFound("Tourist not found");

            var entity = reaction.Reaction(await _context.Reactions.FindAsync(id));
            entity.Id = id;

            _context.Update(entity);
            await _context.SaveChangesAsync();

            return new ApiResponse();
        }

        public async Task<ApiResponse> DeleteReaction(int id, UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized("You don't have permissions");

            var reaction = await _context.Reactions.FindAsync(id);
            if (reaction is null)
                return new NotFound("Reaction not found");

            _context.Reactions.Remove(reaction);
            await _context.SaveChangesAsync();

            return new ApiResponse();
        }

        private static bool CheckPermissions(UserBasic user) =>
            user.Role == Roles.Tourist;
    }
}