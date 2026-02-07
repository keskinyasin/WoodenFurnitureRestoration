using AutoMapper;
using WoodenFurnitureRestoration.Entities;
using WoodenFurnitureRestoration.Shared.DTOs.Restoration;

namespace WoodenFurnitureRestoration.Core.Mappings;

public class RestorationMappingProfile : Profile
{
    public RestorationMappingProfile()
    {
        // Entity → Entity (for updates)
        CreateMap<Restoration, Restoration>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDetails, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.RestorationServices, opt => opt.Ignore())
            .ForMember(dest => dest.BlogPosts, opt => opt.Ignore());

        // Entity → DTO
        CreateMap<Restoration, RestorationDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src =>
                src.Category != null ? src.Category.CategoryName : string.Empty));

        // CreateDTO → Entity
        CreateMap<CreateRestorationDto, Restoration>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.RestorationStatus, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDetails, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.RestorationServices, opt => opt.Ignore())
            .ForMember(dest => dest.BlogPosts, opt => opt.Ignore());

        // UpdateDTO → Entity
        CreateMap<UpdateRestorationDto, Restoration>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDetails, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.RestorationServices, opt => opt.Ignore())
            .ForMember(dest => dest.BlogPosts, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}