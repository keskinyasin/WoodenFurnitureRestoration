using AutoMapper;
using WoodenFurnitureRestoration.Entities;
using WoodenFurnitureRestoration.Shared.DTOs.Supplier;

namespace WoodenFurnitureRestoration.Core.Mappings;

public class SupplierMappingProfile : Profile
{
    public SupplierMappingProfile()
    {
        // Entity → Entity (for updates)
        CreateMap<Supplier, Supplier>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.Addresses, opt => opt.Ignore())
            .ForMember(dest => dest.Products, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterials, opt => opt.Ignore())
            .ForMember(dest => dest.Shippings, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.Payments, opt => opt.Ignore())
            .ForMember(dest => dest.Orders, opt => opt.Ignore())
            .ForMember(dest => dest.Invoices, opt => opt.Ignore())
            .ForMember(dest => dest.Categories, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierCategories, opt => opt.Ignore());

        // Entity → DTO
        CreateMap<Supplier, SupplierDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src =>
                src.Products != null ? src.Products.Count : 0));

        // CreateDTO → Entity
        CreateMap<CreateSupplierDto, Supplier>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.Addresses, opt => opt.Ignore())
            .ForMember(dest => dest.Products, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterials, opt => opt.Ignore())
            .ForMember(dest => dest.Shippings, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.Payments, opt => opt.Ignore())
            .ForMember(dest => dest.Orders, opt => opt.Ignore())
            .ForMember(dest => dest.Invoices, opt => opt.Ignore())
            .ForMember(dest => dest.Categories, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierCategories, opt => opt.Ignore());

        // UpdateDTO → Entity
        CreateMap<UpdateSupplierDto, Supplier>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.Addresses, opt => opt.Ignore())
            .ForMember(dest => dest.Products, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterials, opt => opt.Ignore())
            .ForMember(dest => dest.Shippings, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.Payments, opt => opt.Ignore())
            .ForMember(dest => dest.Orders, opt => opt.Ignore())
            .ForMember(dest => dest.Invoices, opt => opt.Ignore())
            .ForMember(dest => dest.Categories, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierCategories, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}