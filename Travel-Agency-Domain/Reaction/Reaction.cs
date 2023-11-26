using Travel_Agency_Core;
using Travel_Agency_Core.Enums;
using Travel_Agency_Domain.Offers;
using Travel_Agency_Domain.Users;

namespace Travel_Agency_Domain.Reactions;

public class Reaction : Entity
{
    public ReactionState ReactionState { get; set; } = ReactionState.NotSet;

    public int TouristId { get; set; }
    public Tourist Tourist { get; set; } = null!;

    public int OfferId { get; set; }
    public Offer Offer { get; set; } = null!;

    public Reaction(ReactionState reactionState, int touristId, int offerId)
    {
        ReactionState = reactionState;
        TouristId = touristId;
        OfferId = offerId;
    }
}