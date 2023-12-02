using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Payments;

namespace Travel_Agency_DataBase.Configurations;

public class PaymentConfiguration : EntityConfiguration<Payment>
{
    public override void ConfigureEntity(EntityTypeBuilder<Payment> builder)
    {
        builder.HasOne(p => p.UserIdentity).WithMany().HasForeignKey(p => p.UserIdentityId);
    }
}