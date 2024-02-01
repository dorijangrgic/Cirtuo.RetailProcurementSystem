using Cirtuo.RetailProcurementSystem.Persistence;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace Cirtuo.RetailProcurementSystem.Application.Test;

public class IntegrationTestFixture : IAsyncDisposable
{
    private readonly PostgreSqlContainer _container;
    public readonly DbContextOptions<RetailProcurementDbContext> DbContextOptions;
    
    public IntegrationTestFixture()
    {
        _container = new PostgreSqlBuilder().WithDatabase("cirtuo-rps-db").Build();
        _container.StartAsync().GetAwaiter().GetResult();
        
        DbContextOptions = new DbContextOptionsBuilder<RetailProcurementDbContext>()
            .UseNpgsql(_container.GetConnectionString())
            .Options;
        
        using var dbContext = new RetailProcurementDbContext(DbContextOptions);
        dbContext.Database.EnsureCreated();
    }
    
    public async ValueTask DisposeAsync() => await _container.DisposeAsync();
}