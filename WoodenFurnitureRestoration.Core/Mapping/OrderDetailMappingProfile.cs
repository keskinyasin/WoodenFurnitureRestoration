using AutoMapper;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Mappings;

public class OrderDetailMappingProfile : Profile
{
    public OrderDetailMappingProfile()
    {
        CreateMap<OrderDetail, OrderDetail>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.OrderId, opt => opt.Ignore())
            // Navigation properties
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.Restoration, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore());
    }
}