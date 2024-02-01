using AutoMapper;
using Cirtuo.RetailProcurementSystem.Application.SupplierRetailers.Models;
using Cirtuo.RetailProcurementSystem.Domain;

namespace Cirtuo.RetailProcurementSystem.Application.SupplierRetailers.Profiles;

public class SupplierRetailerProfile : Profile
{
    public SupplierRetailerProfile()
    {
        CreateMap<SupplierRetailer, SupplierRetailerDto>().ReverseMap();
    }
}