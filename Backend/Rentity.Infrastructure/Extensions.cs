using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rentify.Domain.Repositories;
using Rentify.Domain.Services;
using Rentity.Infrastructure.Data;
using Rentity.Infrastructure.Repositories;
using Rentity.Infrastructure.Services;

namespace Rentity.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructures(this IServiceCollection services, IConfiguration configuration)
    {
        // Add db context
        var connectionString = configuration.GetConnectionString("DbConnection");
        services.AddDbContext<AppDbContext>(option => option.UseSqlite(connectionString));

        // Add repositories
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IPropertyRepository, PropertyRepository>();

        // Add Logger service
        services.AddSingleton<ILoggerService, LoggerService>();

        return services;
    }
}
