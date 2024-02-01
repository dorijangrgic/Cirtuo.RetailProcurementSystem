using System.Text.Json.Serialization;
using Cirtuo.RetailProcurementSystem.Api.ExceptionHandlers;

namespace Cirtuo.RetailProcurementSystem.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddRestApi(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services
            .AddControllers()
            .AddJsonOptions(opts =>
            {
                var enumConverter = new JsonStringEnumConverter();
                opts.JsonSerializerOptions.Converters.Add(enumConverter);
            });

        services.AddExceptionHandler<BadRequestExceptionHandler>();
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }
}