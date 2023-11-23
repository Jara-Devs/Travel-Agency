using Travel_Agency_Domain.Reactions;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Logic.Request;

public class ReactionRequest
{
    public bool Liked { get; set; }
    public string? Comment { get; set; }
    public int TouristId { get; set; }
    public int OfferId { get; set; }

    public Reaction Reaction(Reaction? reaction = null) 
    {
        reaction ??= new Reaction(this.Liked, this.TouristId, this.OfferId, this.Comment);
        reaction.Liked = this.Liked;
        reaction.TouristId = this.TouristId;
        reaction.OfferId = this.OfferId;
        reaction.Comment = this.Comment;

        return reaction;
    }
}