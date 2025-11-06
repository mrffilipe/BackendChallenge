using BackendChallenge.Application.Services;
using BackendChallenge.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BackendChallenge.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUnityOfWorkService, UnitOfWorkService>();

            return services;
        }
    }
}
