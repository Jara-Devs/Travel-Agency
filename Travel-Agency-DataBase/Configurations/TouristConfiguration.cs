using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Users;

namespace Travel_Agency_DataBase.Configurations;

public class TouristConfiguration : EntityConfiguration<Tourist>
{
    protected override bool ConfigureKey => false;

    public override void ConfigureEntity(EntityTypeBuilder<Tourist> builder)
    {
        builder.HasOne(x => x.UserIdentity).WithOne().HasForeignKey<Tourist>(x => x.UserIdentityId);
    }
}