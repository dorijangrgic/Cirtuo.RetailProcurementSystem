using Microsoft.Extensions.DependencyInjection;

namespace Cirtuo.RetailProcurementSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        // register application services
        return services;
    }
}