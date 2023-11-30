using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Travel_Agency_Core.Enums;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_DataBase.Configurations;

public class HotelOfferConfiguration : EntityConfiguration<HotelOffer>
{
    protected override bool ConfigureKey => false;

    public override void ConfigureEntity(EntityTypeBuilder<HotelOffer> builder)
    {
        builder.HasMany(o => o.Packages).WithMany(p => p.HotelOffers);
        builder.HasOne(o => o.Hotel).WithMany(h => h.Offers).HasForeignKey(o => o.HotelId);
    }
}