using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Payments;

namespace Travel_Agency_DataBase.Configurations;

public class ReserveConfiguration:EntityConfiguration<Reserve>
{
    public override void ConfigureEntity(EntityTypeBuilder<Reserve> builder)
    {
        builder.OwnsMany(r => r.UserIdentities);
        builder.HasOne(r => r.Package).WithMany(p => p.Reserves).HasForeignKey(r => r.PackageId);
    }
}