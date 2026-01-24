using AutoMapper;
using WoodenFurnitureRestoration.Entities;
using WoodenFurnitureRestoration.Shared.DTOs.Category;
using WoodenFurnitureRestoration.Shared.DTOs.Order;
using WoodenFurnitureRestoration.Shared.DTOs.Product;

namespace WoodenFurnitureRestoration.API.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ✅ Category Mapping - COMPLETE
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.CategoryDescription))
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier != null ? src.Supplier.SupplierName : "N/A"));

            CreateMap<CreateCategoryDto, Category>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CategoryDescription, opt => opt.MapFrom(src => src.Description));

            CreateMap<UpdateCategoryDto, Category>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CategoryDescription, opt => opt.MapFrom(src => src.Description));

            // ✅ Product Mapping - COMPLETE
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : "N/A"));

            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            // ✅ Order Mapping - COMPLETE
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.OrderStatus))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? (src.Customer.CustomerFirstName + " " + src.Customer.CustomerLastName) : "N/A"))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.CustomerEmail : "N/A"))
                .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SupplierId))
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier != null ? src.Supplier.SupplierName : "N/A"))
                .ForMember(dest => dest.SupplierMaterialId, opt => opt.MapFrom(src => src.SupplierMaterialId))
                .ForMember(dest => dest.ShippingId, opt => opt.MapFrom(src => src.ShippingId))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.OrderDetails != null ? src.OrderDetails.Sum(od => od.UnitPrice * od.Quantity) : 0));

            CreateMap<CreateOrderDto, Order>()
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.ShippingId, opt => opt.MapFrom(src => src.ShippingId))
                .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SupplierId))
                .ForMember(dest => dest.SupplierMaterialId, opt => opt.MapFrom(src => src.SupplierMaterialId));

            CreateMap<UpdateOrderDto, Order>()
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.ShippingId, opt => opt.MapFrom(src => src.ShippingId));
        }
    }
}