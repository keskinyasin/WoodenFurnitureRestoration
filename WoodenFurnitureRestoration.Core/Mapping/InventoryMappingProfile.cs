using AutoMapper;
using WoodenFurnitureRestoration.Entities;
using WoodenFurnitureRestoration.Shared.DTOs.Inventory;

namespace WoodenFurnitureRestoration.Core.Mappings;

public class InventoryMappingProfile : Profile
{
    public InventoryMappingProfile()
    {
        // Entity → Entity (for updates)
        CreateMap<Inventory, Inventory>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterial, opt => opt.Ignore())
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.ShippingInventories, opt => opt.Ignore());

        // Entity → DTO
        CreateMap<Inventory, InventoryDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src =>
                src.Product != null ? src.Product.ProductName : string.Empty))
            .ForMember(dest => dest.SupplierMaterialName, opt => opt.MapFrom(src =>
                src.SupplierMaterial != null ? src.SupplierMaterial.MaterialName : string.Empty));

        // CreateDTO → Entity
        CreateMap<CreateInventoryDto, Inventory>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.LastUpdate, opt => opt.Ignore())
            .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterial, opt => opt.Ignore())
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.ShippingInventories, opt => opt.Ignore());

        // UpdateDTO → Entity
        CreateMap<UpdateInventoryDto, Inventory>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.LastUpdate, opt => opt.Ignore())
            .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
            .ForMember(dest => dest.ProductId, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterialId, opt => opt.Ignore())
            .ForMember(dest => dest.AddressId, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterial, opt => opt.Ignore())
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.ShippingInventories, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}