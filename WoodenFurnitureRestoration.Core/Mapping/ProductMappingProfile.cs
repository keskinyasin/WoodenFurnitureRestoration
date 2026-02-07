using AutoMapper;
using WoodenFurnitureRestoration.Entities;
using WoodenFurnitureRestoration.Shared.DTOs.Product;

namespace WoodenFurnitureRestoration.Core.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        // Entity → Entity (for updates)
        CreateMap<Product, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierId, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterialId, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterial, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDetails, opt => opt.Ignore())
            .ForMember(dest => dest.Inventories, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.ProductTags, opt => opt.Ignore());

        // Entity → DTO
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src =>
                src.Category != null ? src.Category.CategoryName : string.Empty));

        // CreateDTO → Entity
        CreateMap<CreateProductDto, Product>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterial, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDetails, opt => opt.Ignore())
            .ForMember(dest => dest.Inventories, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.ProductTags, opt => opt.Ignore());

        // UpdateDTO → Entity
        CreateMap<UpdateProductDto, Product>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierId, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterialId, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterial, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDetails, opt => opt.Ignore())
            .ForMember(dest => dest.Inventories, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.ProductTags, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}