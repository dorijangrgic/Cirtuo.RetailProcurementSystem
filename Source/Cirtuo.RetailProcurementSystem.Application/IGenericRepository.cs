using Ardalis.Specification;

namespace Cirtuo.RetailProcurementSystem.Application;

public interface IGenericRepository<T> : IRepositoryBase<T> where T : class
{
}