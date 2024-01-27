using Cirtuo.RetailProcurementSystem.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cirtuo.RetailProcurementSystem.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        services.AddDbContext<RetailProcurementDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("RetailProcurementSystemDb"));
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.UseSnakeCaseNamingConvention();
            if (isDevelopment) options.EnableSensitiveDataLogging();
        });
        
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        
        return services;
    }
}