using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using nexthappen_backend.SavedEvents.Domain.Entities;


namespace nexthappen_backend.SavedEvents.Infrastructure.Persistence.Configuration;

public class SavedEventConfiguration : IEntityTypeConfiguration<SavedEvent>
{
    public void Configure(EntityTypeBuilder<SavedEvent> builder)
    {
        builder.ToTable("SavedEvents");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.EventId).IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();
    }
}