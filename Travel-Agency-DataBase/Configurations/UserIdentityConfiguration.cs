using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain;

namespace Travel_Agency_DataBase.Configurations;

public class UserIdentityConfiguration : EntityConfiguration<UserIdentity>
{
    public override void ConfigureEntity(EntityTypeBuilder<UserIdentity> builder)
    {
        builder.HasIndex(x => new { x.IdentityDocument, x.Nationality }).IsUnique();
    }
}