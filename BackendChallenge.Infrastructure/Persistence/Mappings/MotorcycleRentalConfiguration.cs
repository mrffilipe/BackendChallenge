using BackendChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendChallenge.Infrastructure.Persistence
{
    public class MotorcycleRentalConfiguration : BaseEntityConfiguration<MotorcycleRental>
    {
        public override void Configure(EntityTypeBuilder<MotorcycleRental> builder)
        {
            base.Configure(builder);

            builder.ToTable("motorcycle_rentals");

            builder.HasAlternateKey(x => x.ExternalId);

            builder.Property(x => x.ExternalId)
                .HasColumnName("external_id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.DeliveryPersonId)
                .HasColumnName("delivery_person_id")
                .IsRequired();

            builder.Property(x => x.MotorcycleId)
                .HasColumnName("motorcycle_id")
                .IsRequired();

            builder.Property(x => x.StartDate)
                .HasColumnName("start_date")
                .IsRequired();

            builder.Property(x => x.EndDate)
                .HasColumnName("end_date")
                .IsRequired();

            builder.Property(x => x.EstimatedEndDate)
                .HasColumnName("estimated_end_date")
                .IsRequired();

            builder.Property(x => x.RentalPlan)
                .HasColumnName("rental_plan")
                .IsRequired();
        }
    }
}
