using AutoMapper;
using WoodenFurnitureRestoration.Entities;
using WoodenFurnitureRestoration.Shared.DTOs.Review;

namespace WoodenFurnitureRestoration.Core.Mappings;

public class ReviewMappingProfile : Profile
{
    public ReviewMappingProfile()
    {
        // Entity → Entity (for updates)
        CreateMap<Review, Review>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
            .ForMember(dest => dest.ProductId, opt => opt.Ignore())
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterial, opt => opt.Ignore())
            .ForMember(dest => dest.Restoration, opt => opt.Ignore());

        // Entity → DTO
        CreateMap<Review, ReviewDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src =>
                src.Customer != null ? $"{src.Customer.CustomerFirstName} {src.Customer.CustomerLastName}" : string.Empty))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src =>
                src.Product != null ? src.Product.ProductName : string.Empty))
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src =>
                src.Supplier != null ? src.Supplier.SupplierName : string.Empty))
            .ForMember(dest => dest.RestorationName, opt => opt.MapFrom(src =>
                src.Restoration != null ? src.Restoration.RestorationName : string.Empty));

        // CreateDTO → Entity
        CreateMap<CreateReviewDto, Review>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.ReviewDate, opt => opt.Ignore())
            .ForMember(dest => dest.ReviewStatus, opt => opt.Ignore())
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterial, opt => opt.Ignore())
            .ForMember(dest => dest.Restoration, opt => opt.Ignore());

        // UpdateDTO → Entity
        CreateMap<UpdateReviewDto, Review>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.ReviewDate, opt => opt.Ignore())
            .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
            .ForMember(dest => dest.ProductId, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierId, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterialId, opt => opt.Ignore())
            .ForMember(dest => dest.RestorationId, opt => opt.Ignore())
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierMaterial, opt => opt.Ignore())
            .ForMember(dest => dest.Restoration, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}