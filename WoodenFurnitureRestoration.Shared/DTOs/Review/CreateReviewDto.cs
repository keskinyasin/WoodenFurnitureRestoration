using System.ComponentModel.DataAnnotations;

namespace WoodenFurnitureRestoration.Shared.DTOs.Review;

public class CreateReviewDto
{
    [Required]
    public int CustomerId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public int SupplierId { get; set; }

    [Required]
    public int SupplierMaterialId { get; set; }

    [Required]
    public int RestorationId { get; set; }

    [Required(ErrorMessage = "Yorum açıklaması zorunludur")]
    [StringLength(500)]
    public string ReviewDescription { get; set; } = string.Empty;

    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }
}