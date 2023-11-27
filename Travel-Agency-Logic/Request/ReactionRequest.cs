using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Reactions;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Request;

public class ReactionRequest
{
    public ReactionState ReactionState { get; set; }
    public Guid TouristId { get; set; }
    public Guid OfferId { get; set; }

    public Reaction Reaction(Reaction? reaction = null) 
    {
        reaction ??= new Reaction(this.ReactionState, this.TouristId, this.OfferId);
        reaction.ReactionState = this.ReactionState;
        reaction.TouristId = this.TouristId;
        reaction.OfferId = this.OfferId;

        return reaction;
    }
}