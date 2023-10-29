using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Packages;

namespace Travel_Agency_DataBase.Configurations;

public class PackageConfiguration : EntityConfiguration<Package>
{
    public override void ConfigureEntity(EntityTypeBuilder<Package> builder)
    {
    }
}