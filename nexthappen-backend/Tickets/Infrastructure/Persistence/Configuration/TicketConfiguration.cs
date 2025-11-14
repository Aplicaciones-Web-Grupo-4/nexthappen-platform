using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using nexthappen_backend.Tickets.Domain.Entities;
using nexthappen_backend.Tickets.Domain.ValueObjects;


namespace nexthappen_backend.Tickets.Infrastructure.Persistence.Configuration;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.UserId).IsRequired();
        builder.Property(t => t.EventId).IsRequired();
        builder.Property(t => t.PurchaseDate).IsRequired();

        builder.Property(t => t.Status)
            .HasConversion<int>()
            .IsRequired();
    }
}