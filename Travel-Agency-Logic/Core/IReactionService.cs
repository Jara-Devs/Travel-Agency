using Travel_Agency_Core;
using Travel_Agency_Domain.Services;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Core;

public interface IReactionService
{
    Task<ApiResponse<IdResponse>> CreateReaction(ReactionRequest reaction, UserBasic user);
    Task<ApiResponse> UpdateReaction(int id, ReactionRequest reaction, UserBasic user);
    Task<ApiResponse> DeleteReaction(int id, UserBasic user);
}