using Microsoft.Extensions.DependencyInjection;
using Rentify.Application.Services;
using Rentify.Application.Services.Interfaces;

namespace Rentify.Application;

public static class Extensions
{
    public static IServiceCollection AddApplications(this IServiceCollection services)
    {
        // Injecting MediatR to our DI
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Extensions).Assembly));

        services.AddScoped<ICachingService, CachingService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}