using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;

namespace Travel_Agency_DataBase.Configurations;

public class ImageConfiguration : EntityConfiguration<Image>
{
    public override void ConfigureEntity(EntityTypeBuilder<Image> builder)
    {
        builder.HasIndex(i => i.Url).IsUnique();
    }
}