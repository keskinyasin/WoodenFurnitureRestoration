using AutoMapper;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Mappings;

public class AddressMappingProfile : Profile
{
    public AddressMappingProfile()
    {
        CreateMap<Address, Address>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierId, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            // Navigation properties
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.Inventories, opt => opt.Ignore())
            .ForMember(dest => dest.Payments, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterials, opt => opt.Ignore())
            .ForMember(dest => dest.Shippings, opt => opt.Ignore())
            .ForMember(dest => dest.BlogPosts, opt => opt.Ignore());
    }
}