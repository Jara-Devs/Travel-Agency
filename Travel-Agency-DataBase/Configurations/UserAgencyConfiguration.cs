using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Users;

namespace Travel_Agency_DataBase.Configurations;

public class UserAgencyConfiguration : EntityConfiguration<UserAgency>
{
    protected override bool ConfigureKey => false;

    public override void ConfigureEntity(EntityTypeBuilder<UserAgency> builder)
    {
        builder.HasOne(u => u.Agency)
            .WithMany(a => a.Users)
            .HasForeignKey(u => u.AgencyId);
    }
}