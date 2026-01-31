using AutoMapper;
using RestorationServiceEntity = WoodenFurnitureRestoration.Entities.RestorationService;

namespace WoodenFurnitureRestoration.Core.Mappings;

public class RestorationServiceMappingProfile : Profile
{
    public RestorationServiceMappingProfile()
    {
        CreateMap<RestorationServiceEntity, RestorationServiceEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.RestorationId, opt => opt.Ignore())
            // Navigation properties
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Restoration, opt => opt.Ignore());
    }
}
