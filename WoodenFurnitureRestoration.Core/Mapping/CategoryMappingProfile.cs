using AutoMapper;
using WoodenFurnitureRestoration.Entities;
using WoodenFurnitureRestoration.Shared.DTOs.Category;

namespace WoodenFurnitureRestoration.Core.Mappings;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        // Entity → DTO
        CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.CategoryDescription))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src =>
                src.Supplier != null ? src.Supplier.SupplierName : string.Empty));

        // CreateDTO → Entity
        CreateMap<CreateCategoryDto, Category>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CategoryDescription, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.Products, opt => opt.Ignore())
            .ForMember(dest => dest.Restorations, opt => opt.Ignore())
            .ForMember(dest => dest.RestorationServices, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterials, opt => opt.Ignore())
            .ForMember(dest => dest.BlogPosts, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryTags, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierCategories, opt => opt.Ignore());

        // UpdateDTO → Entity
        CreateMap<UpdateCategoryDto, Category>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CategoryDescription, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierId, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.Products, opt => opt.Ignore())
            .ForMember(dest => dest.Restorations, opt => opt.Ignore())
            .ForMember(dest => dest.RestorationServices, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterials, opt => opt.Ignore())
            .ForMember(dest => dest.BlogPosts, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryTags, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierCategories, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}