using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Travel_Agency_Core.Enums;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_DataBase.Configurations;

public class ExcursionOfferConfiguration : EntityConfiguration<ExcursionOffer>
{
    protected override bool ConfigureKey => false;

    public override void ConfigureEntity(EntityTypeBuilder<ExcursionOffer> builder)
    {
        builder.HasOne(o => o.Excursion).WithMany(e => e.Offers).HasForeignKey(o => o.ExcursionId);
        builder.Property(o => o.Facilities).HasConversion(f => JsonConvert.SerializeObject(f),
            f => JsonConvert.DeserializeObject<List<ExcursionFacility>>(f)!,
            new ValueComparer<List<ExcursionFacility>>((f1, f2) => (f1 != null && f2 != null) && f1.SequenceEqual(f2),
                f => f.Aggregate(0, (a, v) => HashCode.Combine(a, f.GetHashCode())),
                f => f.ToList()));
    }
}