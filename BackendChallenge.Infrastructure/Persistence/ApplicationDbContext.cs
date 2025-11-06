using BackendChallenge.Domain.Common;
using BackendChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendChallenge.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<DeliveryPerson> DeliveryDrivers { get; set; }
        public DbSet<MotorcycleRental> MotorcycleRentals { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.SetCreatedAt();
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.SetUpdatedAt();
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
