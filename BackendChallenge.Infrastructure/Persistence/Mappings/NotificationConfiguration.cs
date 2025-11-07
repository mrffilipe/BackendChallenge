using BackendChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendChallenge.Infrastructure.Persistence
{
    public class NotificationConfiguration : BaseEntityConfiguration<Notification>
    {
        public override void Configure(EntityTypeBuilder<Notification> builder)
        {
            base.Configure(builder);

            builder.ToTable("notifications");

            builder.Property(x => x.MotorcycleId)
                .HasColumnName("motorcycle_id")
                .IsRequired();

            builder.Property(x => x.MotorcycleExternalId)
                .HasColumnName("motorcycle_external_id")
                .IsRequired();

            builder.Property(x => x.Type)
                .HasColumnName("type")
                .IsRequired();
        }
    }
}
