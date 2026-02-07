using AutoMapper;
using WoodenFurnitureRestoration.Entities;
using WoodenFurnitureRestoration.Shared.DTOs.Shipping;

namespace WoodenFurnitureRestoration.Core.Mappings;

public class ShippingMappingProfile : Profile
{
    public ShippingMappingProfile()
    {
        // Entity → Entity (for updates)
        CreateMap<Shipping, Shipping>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.OrderId, opt => opt.Ignore())
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterial, opt => opt.Ignore())
            .ForMember(dest => dest.ShippingPayments, opt => opt.Ignore())
            .ForMember(dest => dest.ShippingProducts, opt => opt.Ignore())
            .ForMember(dest => dest.ShippingInventories, opt => opt.Ignore())
            .ForMember(dest => dest.ShippingTags, opt => opt.Ignore())
            .ForMember(dest => dest.Orders, opt => opt.Ignore())
            .ForMember(dest => dest.Payments, opt => opt.Ignore())
            .ForMember(dest => dest.Invoices, opt => opt.Ignore());

        // Entity → DTO
        CreateMap<Shipping, ShippingDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.ShippingType, opt => opt.MapFrom(src => src.ShippingType.ToString()))
            .ForMember(dest => dest.ShippingStatus, opt => opt.MapFrom(src => src.ShippingStatus.ToString()))
            .ForMember(dest => dest.AddressCity, opt => opt.MapFrom(src =>
                src.Address != null ? src.Address.City : string.Empty))
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src =>
                src.Supplier != null ? src.Supplier.SupplierName : string.Empty));

        // CreateDTO → Entity
        CreateMap<CreateShippingDto, Shipping>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.ShippingStatus, opt => opt.Ignore())
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterial, opt => opt.Ignore())
            .ForMember(dest => dest.ShippingPayments, opt => opt.Ignore())
            .ForMember(dest => dest.ShippingProducts, opt => opt.Ignore())
            .ForMember(dest => dest.ShippingInventories, opt => opt.Ignore())
            .ForMember(dest => dest.ShippingTags, opt => opt.Ignore())
            .ForMember(dest => dest.Orders, opt => opt.Ignore())
            .ForMember(dest => dest.Payments, opt => opt.Ignore())
            .ForMember(dest => dest.Invoices, opt => opt.Ignore());

        // UpdateDTO → Entity
        CreateMap<UpdateShippingDto, Shipping>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.OrderId, opt => opt.Ignore())
            .ForMember(dest => dest.AddressId, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierId, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterialId, opt => opt.Ignore())
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterial, opt => opt.Ignore())
            .ForMember(dest => dest.ShippingPayments, opt => opt.Ignore())
            .ForMember(dest => dest.ShippingProducts, opt => opt.Ignore())
            .ForMember(dest => dest.ShippingInventories, opt => opt.Ignore())
            .ForMember(dest => dest.ShippingTags, opt => opt.Ignore())
            .ForMember(dest => dest.Orders, opt => opt.Ignore())
            .ForMember(dest => dest.Payments, opt => opt.Ignore())
            .ForMember(dest => dest.Invoices, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}