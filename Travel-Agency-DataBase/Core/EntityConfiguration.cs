using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_Core;

namespace Travel_Agency_DataBase.Core;

public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(o => o.Id).IsUnique();

        ConfigureEntity(builder);
    }

    public abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
}