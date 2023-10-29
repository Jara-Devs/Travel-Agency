using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Users;

namespace Travel_Agency_DataBase.Configurations;

public class UserConfiguration : EntityConfiguration<User>
{
    public override void ConfigureEntity(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => u.Email).IsUnique();
    }
}