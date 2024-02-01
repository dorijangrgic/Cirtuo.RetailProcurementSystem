using AutoMapper;
using Cirtuo.RetailProcurementSystem.Application.Common.Models;
using Cirtuo.RetailProcurementSystem.Domain;

namespace Cirtuo.RetailProcurementSystem.Application.Common.Profiles;

public class RetailerProfile : Profile
{
    public RetailerProfile()
    {
        CreateMap<Retailer, RetailerDto>();
    }
}