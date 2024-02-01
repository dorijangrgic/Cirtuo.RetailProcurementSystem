using AutoMapper;
using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Models;
using Cirtuo.RetailProcurementSystem.Domain;

namespace Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Profiles;

public class SupplierStoreItemProfile : Profile
{
    public SupplierStoreItemProfile()
    {
        CreateMap<SupplierStoreItem, SupplierStoreItemDto>().ReverseMap();
    }
}