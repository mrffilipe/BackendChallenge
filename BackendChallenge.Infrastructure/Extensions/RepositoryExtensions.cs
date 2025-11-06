using BackendChallenge.Application.UseCases;
using BackendChallenge.Domain.Repositories;
using BackendChallenge.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace BackendChallenge.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMotorcycleQueryRepository, MotorcycleQueryRepository>();
            services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();

            services.AddScoped<IDeliveryPersonQueryRepository, DeliveryPersonQueryRepository>();
            services.AddScoped<IDeliveryPersonRepository, DeliveryPersonRepository>();

            services.AddScoped<IMotorcycleRentalQueryRepository, MotorcycleRentalQueryRepository>();
            services.AddScoped<IMotorcycleRentalRepository, MotorcycleRentalRepository>();

            return services;
        }
    }
}
