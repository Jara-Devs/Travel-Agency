using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain;

namespace Travel_Agency_DataBase.Configurations;

public class AgencyConfiguration : EntityConfiguration<Agency>
{
    public override void ConfigureEntity(EntityTypeBuilder<Agency> builder)
    {
        builder.HasIndex(a => a.Name).IsUnique();
    }
}