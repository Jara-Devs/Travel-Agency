using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Travel_Agency_Core.Enums;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_DataBase.Configurations;

public class FlightOfferConfiguration : EntityConfiguration<FlightOffer>
{
    protected override bool ConfigureKey => false;

    public override void ConfigureEntity(EntityTypeBuilder<FlightOffer> builder)
    {
        builder.HasMany(o => o.Packages).WithMany(p => p.FlightOffers);
        builder.HasOne(o => o.Flight).WithMany(f => f.Offers).HasForeignKey(o => o.FlightId);
        builder.Property(o => o.Facilities).HasConversion(f => JsonConvert.SerializeObject(f),
            f => JsonConvert.DeserializeObject<List<FlightFacility>>(f)!,
            new ValueComparer<List<FlightFacility>>((f1, f2) => f1 != null && f2 != null && f1.SequenceEqual(f2),
                f => f.Aggregate(0, (a, v) => HashCode.Combine(a, f.GetHashCode())),
                f => f.ToList()));
    }
}