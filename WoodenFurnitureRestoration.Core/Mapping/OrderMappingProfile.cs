using AutoMapper;
using WoodenFurnitureRestoration.Entities;
using WoodenFurnitureRestoration.Shared.DTOs.Order;

namespace WoodenFurnitureRestoration.Core.Mappings;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        // Entity → Entity (for updates)
        CreateMap<Order, Order>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterial, opt => opt.Ignore())
            .ForMember(dest => dest.Shipping, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDetails, opt => opt.Ignore())
            .ForMember(dest => dest.Payments, opt => opt.Ignore())
            .ForMember(dest => dest.Invoices, opt => opt.Ignore())
            .ForMember(dest => dest.OrderTags, opt => opt.Ignore());

        // Entity → DTO
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.OrderStatus))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src =>
                src.Customer != null ? $"{src.Customer.CustomerFirstName} {src.Customer.CustomerLastName}" : string.Empty))
            .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src =>
                src.Customer != null ? src.Customer.CustomerEmail : string.Empty))
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src =>
                src.Supplier != null ? src.Supplier.SupplierName : string.Empty))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src =>
                src.OrderDetails != null ? src.OrderDetails.Sum(od => od.Quantity * od.UnitPrice) : 0));

        // CreateDTO → Entity
        CreateMap<CreateOrderDto, Order>()
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDate, opt => opt.Ignore())
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterial, opt => opt.Ignore())
            .ForMember(dest => dest.Shipping, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDetails, opt => opt.Ignore())
            .ForMember(dest => dest.Payments, opt => opt.Ignore())
            .ForMember(dest => dest.Invoices, opt => opt.Ignore())
            .ForMember(dest => dest.OrderTags, opt => opt.Ignore());

        // UpdateDTO → Entity
        CreateMap<UpdateOrderDto, Order>()
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDate, opt => opt.Ignore())
            .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierId, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterialId, opt => opt.Ignore())
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterial, opt => opt.Ignore())
            .ForMember(dest => dest.Shipping, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDetails, opt => opt.Ignore())
            .ForMember(dest => dest.Payments, opt => opt.Ignore())
            .ForMember(dest => dest.Invoices, opt => opt.Ignore())
            .ForMember(dest => dest.OrderTags, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}