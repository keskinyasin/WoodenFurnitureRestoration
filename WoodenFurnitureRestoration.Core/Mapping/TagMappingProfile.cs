using AutoMapper;
using WoodenFurnitureRestoration.Entities;
using WoodenFurnitureRestoration.Shared.DTOs.Tag;

namespace WoodenFurnitureRestoration.Core.Mappings;

public class TagMappingProfile : Profile
{
    public TagMappingProfile()
    {
        // Entity → Entity (for updates)
        CreateMap<Tag, Tag>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.BlogPostTags, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryTags, opt => opt.Ignore())
            .ForMember(dest => dest.InvoiceTags, opt => opt.Ignore())
            .ForMember(dest => dest.OrderTags, opt => opt.Ignore())
            .ForMember(dest => dest.PaymentTags, opt => opt.Ignore())
            .ForMember(dest => dest.ProductTags, opt => opt.Ignore())
            .ForMember(dest => dest.ShippingTags, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterialTags, opt => opt.Ignore());

        // Entity → DTO
        CreateMap<Tag, TagDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.UsageCount, opt => opt.MapFrom(src => GetUsageCount(src)));

        // CreateDTO → Entity
        CreateMap<CreateTagDto, Tag>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.BlogPostTags, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryTags, opt => opt.Ignore())
            .ForMember(dest => dest.InvoiceTags, opt => opt.Ignore())
            .ForMember(dest => dest.OrderTags, opt => opt.Ignore())
            .ForMember(dest => dest.PaymentTags, opt => opt.Ignore())
            .ForMember(dest => dest.ProductTags, opt => opt.Ignore())
            .ForMember(dest => dest.ShippingTags, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterialTags, opt => opt.Ignore());

        // UpdateDTO → Entity
        CreateMap<UpdateTagDto, Tag>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.BlogPostTags, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryTags, opt => opt.Ignore())
            .ForMember(dest => dest.InvoiceTags, opt => opt.Ignore())
            .ForMember(dest => dest.OrderTags, opt => opt.Ignore())
            .ForMember(dest => dest.PaymentTags, opt => opt.Ignore())
            .ForMember(dest => dest.ProductTags, opt => opt.Ignore())
            .ForMember(dest => dest.ShippingTags, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterialTags, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }

    private static int GetUsageCount(Tag tag)
    {
        int count = 0;
        count += tag.BlogPostTags?.Count ?? 0;
        count += tag.CategoryTags?.Count ?? 0;
        count += tag.InvoiceTags?.Count ?? 0;
        count += tag.OrderTags?.Count ?? 0;
        count += tag.PaymentTags?.Count ?? 0;
        count += tag.ProductTags?.Count ?? 0;
        count += tag.ShippingTags?.Count ?? 0;
        count += tag.SupplierMaterialTags?.Count ?? 0;
        return count;
    }
}