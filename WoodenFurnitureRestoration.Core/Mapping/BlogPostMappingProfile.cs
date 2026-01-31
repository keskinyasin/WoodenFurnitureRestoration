using AutoMapper;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Mappings;

public class BlogPostMappingProfile : Profile
{
    public BlogPostMappingProfile()
    {
        CreateMap<BlogPost, BlogPost>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.CustomerId, opt => opt.Ignore());
    }
}