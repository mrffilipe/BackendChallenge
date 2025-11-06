using BackendChallenge.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace BackendChallenge.Infrastructure.Extensions
{
    public static class UseCaseExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IAdminRegisterMotorcycle, AdminRegisterMotorcycle>();
            services.AddScoped<IAdminRemovesMotorcycleById, AdminRemovesMotorcycleById>();
            services.AddScoped<IAdminSearchesForMotorcycleById, AdminSearchesForMotorcycleById>();
            services.AddScoped<IAdminSearchesForMotorcycleByPlate, AdminSearchesForMotorcycleByPlate>();
            services.AddScoped<IAdminUpdatesMotorcyclePlate, AdminUpdatesMotorcyclePlate>();

            services.AddScoped<IRegisterDeliveryPerson, RegisterDeliveryPerson>();
            services.AddScoped<IUpdateYourDriversLicensePhoto, UpdateYourDriversLicensePhoto>();

            services.AddScoped<IRegisterMotorcycleRental, RegisterMotorcycleRental>();
            services.AddScoped<ISearcheForMotorcycleRentalById, SearcheForMotorcycleRentalById>();
            services.AddScoped<IUpdateReturnDate, UpdateReturnDate>();

            return services;
        }
    }
}
