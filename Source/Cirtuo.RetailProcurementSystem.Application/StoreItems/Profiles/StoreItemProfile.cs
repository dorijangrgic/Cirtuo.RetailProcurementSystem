using AutoMapper;
using Cirtuo.RetailProcurementSystem.Application.StoreItems.Models;
using Cirtuo.RetailProcurementSystem.Domain;

namespace Cirtuo.RetailProcurementSystem.Application.StoreItems.Profiles;

public class StoreItemProfile : Profile
{
    public StoreItemProfile()
    {
        CreateMap<StoreItem, StoreItemDto>().ReverseMap();
    }
}