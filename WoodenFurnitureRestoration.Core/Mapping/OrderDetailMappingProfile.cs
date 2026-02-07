using AutoMapper;
using WoodenFurnitureRestoration.Entities;
using WoodenFurnitureRestoration.Shared.DTOs.OrderDetail;

namespace WoodenFurnitureRestoration.Core.Mappings;

public class OrderDetailMappingProfile : Profile
{
    public OrderDetailMappingProfile()
    {
        // Entity → Entity (for updates)
        CreateMap<OrderDetail, OrderDetail>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.OrderId, opt => opt.Ignore())
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.Restoration, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore());

        // Entity → DTO
        CreateMap<OrderDetail, OrderDetailDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.RestorationName, opt => opt.MapFrom(src =>
                src.Restoration != null ? src.Restoration.RestorationName : string.Empty))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src =>
                src.Product != null ? src.Product.ProductName : string.Empty));

        // CreateDTO → Entity
        CreateMap<CreateOrderDetailDto, OrderDetail>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.Restoration, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore());

        // UpdateDTO → Entity
        CreateMap<UpdateOrderDetailDto, OrderDetail>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.OrderId, opt => opt.Ignore())
            .ForMember(dest => dest.ProductId, opt => opt.Ignore())
            .ForMember(dest => dest.RestorationId, opt => opt.Ignore())
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.Restoration, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}