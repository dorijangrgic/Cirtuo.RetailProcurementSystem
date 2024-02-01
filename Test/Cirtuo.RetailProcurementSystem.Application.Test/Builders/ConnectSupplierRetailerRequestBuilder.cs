using Cirtuo.RetailProcurementSystem.Application.SupplierRetailers.Models;

namespace Cirtuo.RetailProcurementSystem.Application.Test.Builders;

public class ConnectSupplierRetailerRequestBuilder
{
    private int _retailerId;
    private List<int> _supplierIds;
    
    public static ConnectSupplierRetailerRequestBuilder Default()
    {
        return new ConnectSupplierRetailerRequestBuilder()
            .WithRetailerId(1)
            .WithSupplierIds(new List<int> { 1, 2, 3 });
    }
    
    public ConnectSupplierRetailerRequestBuilder WithRetailerId(int retailerId)
    {
        _retailerId = retailerId;
        return this;
    }
    
    public ConnectSupplierRetailerRequestBuilder WithSupplierIds(List<int> supplierIds)
    {
        _supplierIds = supplierIds;
        return this;
    }
    
    public ConnectSupplierRetailerRequest Build() => new(_retailerId, _supplierIds);
}