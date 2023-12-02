using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain;

namespace Travel_Agency_DataBase.Configurations;

public class CityConfiguration : EntityConfiguration<Agency>
{
    public override void ConfigureEntity(EntityTypeBuilder<Agency> builder)
    {
    }
}