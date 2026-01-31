using AutoMapper;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Mappings;

public class TagMappingProfile : Profile
{
    public TagMappingProfile()
    {
        CreateMap<Tag, Tag>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            // Join table collections
            .ForMember(dest => dest.BlogPostTags, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryTags, opt => opt.Ignore())
            .ForMember(dest => dest.InvoiceTags, opt => opt.Ignore())
            .ForMember(dest => dest.OrderTags, opt => opt.Ignore())
            .ForMember(dest => dest.PaymentTags, opt => opt.Ignore())
            .ForMember(dest => dest.ProductTags, opt => opt.Ignore())
            .ForMember(dest => dest.ShippingTags, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterialTags, opt => opt.Ignore());
    }
}