using AutoMapper;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Mappings;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierId, opt => opt.Ignore())
            .ForMember(dest => dest.Products, opt => opt.Ignore())
            .ForMember(dest => dest.Restorations, opt => opt.Ignore())
            .ForMember(dest => dest.RestorationServices, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterials, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierCategories, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryTags, opt => opt.Ignore())
            .ForMember(dest => dest.BlogPosts, opt => opt.Ignore())
            .ForMember(dest => dest.Tags, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore());
    }
}