using AutoMapper;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Mappings;

public class ShippingMappingProfile : Profile
{
    public ShippingMappingProfile()
    {
        CreateMap<Shipping, Shipping>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.OrderId, opt => opt.Ignore())
            // Navigation properties
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
    }
}