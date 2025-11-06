using BackendChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendChallenge.Infrastructure.Persistence
{
    public class MotorcycleConfiguration : BaseEntityConfiguration<Motorcycle>
    {
        public override void Configure(EntityTypeBuilder<Motorcycle> builder)
        {
            base.Configure(builder);

            builder.ToTable("motorcycles");

            builder.HasAlternateKey(x => x.ExternalId);

            builder.HasAlternateKey(x => x.Plate);

            builder.Property(x => x.ExternalId)
                .HasColumnName("external_id")
                .IsRequired();

            builder.Property(x => x.Year)
                .HasColumnName("year")
                .IsRequired();

            builder.Property(x => x.Model)
                .HasColumnName("model")
                .IsRequired();

            builder.Property(x => x.Plate)
                .HasColumnName("plate")
                .IsRequired();
        }
    }
}
