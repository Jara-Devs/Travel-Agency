using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_Core;

namespace Travel_Agency_DataBase.Core;

public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
{
    protected virtual bool ConfigureKey => true;

    public void Configure(EntityTypeBuilder<T> builder)
    {
        if (ConfigureKey)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(o => o.Id).IsUnique();
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }

        ConfigureEntity(builder);
    }

    public abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
}