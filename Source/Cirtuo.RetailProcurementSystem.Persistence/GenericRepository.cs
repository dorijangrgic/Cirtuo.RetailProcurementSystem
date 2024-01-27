using Ardalis.Specification.EntityFrameworkCore;
using Cirtuo.RetailProcurementSystem.Application;

namespace Cirtuo.RetailProcurementSystem.Persistence;

public class GenericRepository<T> : RepositoryBase<T>, IGenericRepository<T> where T : class
{
    public GenericRepository(RetailProcurementDbContext dbContext) : base(dbContext)
    {
    }
}