using nexthappen_backend.EventDiscovery.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace nexthappen_backend.EventDiscovery.Infrastructure.Persistence.Configuration;

public class DiscoveryEventConfiguration : IEntityTypeConfiguration<DiscoveryEvent>
{
    public void Configure(EntityTypeBuilder<DiscoveryEvent> builder)
    {
        builder.ToTable("DiscoveryEvents");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(e => e.StartDate)
            .IsRequired();

        builder.Property(e => e.IsPublic)
            .IsRequired();

        builder.Property(e => e.Status)
            .HasConversion<int>()
            .IsRequired();
    }
}