using AutoMapper;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Mappings;

public class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<Customer, Customer>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.CustomerPassword, opt => opt.Ignore())
            // Navigation properties
            .ForMember(dest => dest.Addresses, opt => opt.Ignore())
            .ForMember(dest => dest.BlogPosts, opt => opt.Ignore())
            .ForMember(dest => dest.Orders, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore());
    }
}