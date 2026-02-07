using AutoMapper;
using WoodenFurnitureRestoration.Entities;
using WoodenFurnitureRestoration.Shared.DTOs.BlogPost;

namespace WoodenFurnitureRestoration.Core.Mappings;

public class BlogPostMappingProfile : Profile
{
    public BlogPostMappingProfile()
    {
        // Entity → DTO
        CreateMap<BlogPost, BlogPostDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src =>
                src.Category != null ? src.Category.CategoryName : string.Empty))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src =>
                src.Customer != null ? $"{src.Customer.CustomerFirstName} {src.Customer.CustomerLastName}" : null));

        // CreateDTO → Entity
        CreateMap<CreateBlogPostDto, BlogPost>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.Shipping, opt => opt.Ignore())
            .ForMember(dest => dest.Restoration, opt => opt.Ignore())
            .ForMember(dest => dest.Review, opt => opt.Ignore())
            .ForMember(dest => dest.BlogPostTags, opt => opt.Ignore());

        // UpdateDTO → Entity
        CreateMap<UpdateBlogPostDto, BlogPost>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.Shipping, opt => opt.Ignore())
            .ForMember(dest => dest.Restoration, opt => opt.Ignore())
            .ForMember(dest => dest.Review, opt => opt.Ignore())
            .ForMember(dest => dest.BlogPostTags, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}