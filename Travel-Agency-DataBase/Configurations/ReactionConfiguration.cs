using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Reactions;

namespace Travel_Agency_DataBase.Configurations;

public class ReactionConfiguration : EntityConfiguration<Reaction>
{
    public override void ConfigureEntity(EntityTypeBuilder<Reaction> builder)
    {
        builder.HasIndex(r => new { r.OfferId, r.TouristId }).IsUnique();
        builder.HasOne(r => r.Offer).WithMany(o => o.Reactions).HasForeignKey(r => r.OfferId);
        builder.HasOne(o => o.Tourist).WithMany(t => t.Reactions).HasForeignKey(o => o.TouristId);
    }
}