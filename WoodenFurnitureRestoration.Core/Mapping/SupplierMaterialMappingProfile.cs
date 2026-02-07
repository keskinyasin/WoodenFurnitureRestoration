using AutoMapper;
using WoodenFurnitureRestoration.Entities;
using WoodenFurnitureRestoration.Shared.DTOs.SupplierMaterial;

namespace WoodenFurnitureRestoration.Core.Mappings;

public class SupplierMaterialMappingProfile : Profile
{
    public SupplierMaterialMappingProfile()
    {
        // Entity → Entity (for updates)
        CreateMap<SupplierMaterial, SupplierMaterial>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierId, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.Orders, opt => opt.Ignore())
            .ForMember(dest => dest.Payments, opt => opt.Ignore())
            .ForMember(dest => dest.Shippings, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.Inventories, opt => opt.Ignore())
            .ForMember(dest => dest.Invoices, opt => opt.Ignore());

        // Entity → DTO
        CreateMap<SupplierMaterial, SupplierMaterialDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src =>
                src.Supplier != null ? src.Supplier.SupplierName : string.Empty))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src =>
                src.Category != null ? src.Category.CategoryName : string.Empty));

        // CreateDTO → Entity
        CreateMap<CreateSupplierMaterialDto, SupplierMaterial>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.MaterialStatus, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.Orders, opt => opt.Ignore())
            .ForMember(dest => dest.Payments, opt => opt.Ignore())
            .ForMember(dest => dest.Shippings, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.Inventories, opt => opt.Ignore())
            .ForMember(dest => dest.Invoices, opt => opt.Ignore());

        // UpdateDTO → Entity
        CreateMap<UpdateSupplierMaterialDto, SupplierMaterial>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierId, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
            .ForMember(dest => dest.AddressId, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.Orders, opt => opt.Ignore())
            .ForMember(dest => dest.Payments, opt => opt.Ignore())
            .ForMember(dest => dest.Shippings, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.Inventories, opt => opt.Ignore())
            .ForMember(dest => dest.Invoices, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}