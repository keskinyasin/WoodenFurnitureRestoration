using AutoMapper;
using WoodenFurnitureRestoration.Entities;
using WoodenFurnitureRestoration.Shared.DTOs.Address;

namespace WoodenFurnitureRestoration.Core.Mappings;

public class AddressMappingProfile : Profile
{
    public AddressMappingProfile()
    {
        // Entity → DTO
        CreateMap<Address, AddressDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src =>
                src.Customer != null ? $"{src.Customer.CustomerFirstName} {src.Customer.CustomerLastName}" : null))
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src =>
                src.Supplier != null ? src.Supplier.SupplierName : null));

        // CreateDTO → Entity
        CreateMap<CreateAddressDto, Address>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.Payments, opt => opt.Ignore())
            .ForMember(dest => dest.Shippings, opt => opt.Ignore())
            .ForMember(dest => dest.Inventories, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterials, opt => opt.Ignore());

        // UpdateDTO → Entity
        CreateMap<UpdateAddressDto, Address>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierId, opt => opt.Ignore())
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.Payments, opt => opt.Ignore())
            .ForMember(dest => dest.Shippings, opt => opt.Ignore())
            .ForMember(dest => dest.Inventories, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterials, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}