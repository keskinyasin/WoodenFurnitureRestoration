using AutoMapper;
using WoodenFurnitureRestoration.Entities;
using WoodenFurnitureRestoration.Shared.DTOs.Invoice;

namespace WoodenFurnitureRestoration.Core.Mappings;

public class InvoiceMappingProfile : Profile
{
    public InvoiceMappingProfile()
    {
        // Entity → Entity (for updates)
        CreateMap<Invoice, Invoice>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.OrderId, opt => opt.Ignore())
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.Payment, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterial, opt => opt.Ignore())
            .ForMember(dest => dest.Shipping, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.InvoiceTags, opt => opt.Ignore());

        // Entity → DTO
        CreateMap<Invoice, InvoiceDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.NetAmount, opt => opt.MapFrom(src => src.TotalAmount - src.Discount))
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src =>
                src.Supplier != null ? src.Supplier.SupplierName : string.Empty));

        // CreateDTO → Entity
        CreateMap<CreateInvoiceDto, Invoice>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.Payment, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterial, opt => opt.Ignore())
            .ForMember(dest => dest.Shipping, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.InvoiceTags, opt => opt.Ignore());

        // UpdateDTO → Entity
        CreateMap<UpdateInvoiceDto, Invoice>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.OrderId, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierId, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterialId, opt => opt.Ignore())
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.Payment, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterial, opt => opt.Ignore())
            .ForMember(dest => dest.Shipping, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.InvoiceTags, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}