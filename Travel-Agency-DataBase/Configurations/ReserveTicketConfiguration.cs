using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Payments;

namespace Travel_Agency_DataBase.Configurations;

public class ReserveTicketConfiguration : EntityConfiguration<ReserveTicket>
{
    protected override bool ConfigureKey => false;

    public override void ConfigureEntity(EntityTypeBuilder<ReserveTicket> builder)
    {
        builder.HasOne(r => r.User).WithMany(u => u.Reserves).HasForeignKey(r => r.UserId);
        builder.HasOne(r => r.Payment).WithMany(p => p.ReserveTickets).HasForeignKey(r => r.PaymentId);
    }
}