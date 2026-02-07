using AutoMapper;
using WoodenFurnitureRestoration.Shared.DTOs.RestorationService;
using RestorationServiceEntity = WoodenFurnitureRestoration.Entities.RestorationService;

namespace WoodenFurnitureRestoration.Core.Mappings;

public class RestorationServiceMappingProfile : Profile
{
    public RestorationServiceMappingProfile()
    {
        // Entity → Entity (for updates)
        CreateMap<RestorationServiceEntity, RestorationServiceEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Restoration, opt => opt.Ignore());

        // Entity → DTO
        CreateMap<RestorationServiceEntity, RestorationServiceDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src =>
                src.Category != null ? src.Category.CategoryName : string.Empty))
            .ForMember(dest => dest.RestorationName, opt => opt.MapFrom(src =>
                src.Restoration != null ? src.Restoration.RestorationName : string.Empty));

        // CreateDTO → Entity
        CreateMap<CreateRestorationServiceDto, RestorationServiceEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.RestorationServiceStatus, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Restoration, opt => opt.Ignore());

        // UpdateDTO → Entity
        CreateMap<UpdateRestorationServiceDto, RestorationServiceEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.RestorationId, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Restoration, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}