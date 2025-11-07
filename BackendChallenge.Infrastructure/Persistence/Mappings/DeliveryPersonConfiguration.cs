using BackendChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendChallenge.Infrastructure.Persistence
{
    public class DeliveryPersonConfiguration : BaseEntityConfiguration<DeliveryPerson>
    {
        public override void Configure(EntityTypeBuilder<DeliveryPerson> builder)
        {
            base.Configure(builder);

            builder.ToTable("delivery_drivers");

            builder.HasIndex(x => x.ExternalId)
                .IsUnique();

            builder.HasIndex(x => x.Cnpj)
                .IsUnique();

            builder.HasIndex(x => x.DriversLicense)
                .IsUnique();

            builder.Property(x => x.ExternalId)
                .HasColumnName("external_id")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(x => x.Cnpj)
                .HasColumnName("cnpj")
                .IsRequired();

            builder.Property(x => x.DateOfBirth)
                .HasColumnName("date_of_birth")
                .IsRequired();

            builder.Property(x => x.DriversLicense)
                .HasColumnName("drivers_license")
                .IsRequired();

            builder.Property(x => x.TypeOfDriversLicense)
                .HasColumnName("type_of_drivers_license")
                .IsRequired();

            builder.Property(x => x.UrlOfTheDriversLicenseImage)
                .HasColumnName("url_of_the_drivers_license_image")
                .IsRequired();
        }
    }
}
