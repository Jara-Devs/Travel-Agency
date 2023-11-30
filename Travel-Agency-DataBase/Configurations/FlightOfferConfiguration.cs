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
    }
}