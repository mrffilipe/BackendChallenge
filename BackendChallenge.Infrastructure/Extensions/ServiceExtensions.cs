using BackendChallenge.Application.Services;
using BackendChallenge.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BackendChallenge.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration cfg)
        {
            services.AddScoped<IUnityOfWorkService, UnitOfWorkService>();

            services.AddSingleton<IMessageBus, RabbitMqMessageBus>();

            return services;
        }
    }
}
