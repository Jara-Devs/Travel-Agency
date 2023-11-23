using Travel_Agency_Core;
using Travel_Agency_Domain.Offers;
using Travel_Agency_Domain.Users;

namespace Travel_Agency_Domain.Reaction;

public class Reaction : Entity
{

    public bool Liked { get; set; }
    public string? Comment { get; set; }

    public int TouristId { get; set; }
    public Tourist Tourist { get; set; } = null!;

    public int OfferId { get; set; }
    public Offer Offer { get; set; } = null!;

    public Reaction(bool liked, int touristId, int offerId, string? comment = null)
    {
        Liked = liked;
        TouristId = touristId;
        OfferId = offerId;
        Comment = comment;
    }
}