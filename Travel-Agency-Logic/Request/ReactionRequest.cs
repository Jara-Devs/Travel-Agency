using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Reactions;

namespace Travel_Agency_Logic.Request;

public class ReactionRequest
{
    public ReactionState ReactionState { get; set; }
    public Guid TouristId { get; set; }
    public Guid OfferId { get; set; }

    public Reaction Reaction(Reaction? reaction = null)
    {
        reaction ??= new Reaction(ReactionState, TouristId, OfferId);
        reaction.ReactionState = ReactionState;
        reaction.TouristId = TouristId;
        reaction.OfferId = OfferId;

        return reaction;
    }
}