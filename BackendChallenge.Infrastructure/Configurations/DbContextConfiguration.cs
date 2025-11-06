using BackendChallenge.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BackendChallenge.Infrastructure.Configurations
{
    public static class DbContextConfiguration
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();

                var connectionString = configuration.GetConnectionString("DefaultConnection")
                    ?? throw new Exception("Variável de ambiente 'DefaultConnection' não encontrada");

                options.UseNpgsql(connectionString);
            });

            return services;
        }
    }
}
