using Cirtuo.RetailProcurementSystem.Application.Common.Services;
using Cirtuo.RetailProcurementSystem.Application.StoreItems.Services;
using Cirtuo.RetailProcurementSystem.Application.SupplierRetailers.Services;
using Cirtuo.RetailProcurementSystem.Application.Suppliers.Services;
using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Service;
using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cirtuo.RetailProcurementSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IStoreItemService, StoreItemService>();
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<ISupplierStoreItemService, SupplierStoreItemService>();
        services.AddScoped<ISupplierRetailerService, SupplierRetailerService>();
        services.AddSingleton<IDateTimeService, DateTimeService>();
        
        return services;
    }
}