using System.ComponentModel.DataAnnotations;

namespace WoodenFurnitureRestoration.Shared.DTOs.SupplierMaterial;

public class CreateSupplierMaterialDto
{
    [Range(0, int.MaxValue)]
    public int MaterialQuantity { get; set; }

    [Required(ErrorMessage = "Malzeme adı zorunludur")]
    [StringLength(150)]
    public string MaterialName { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal MaterialPrice { get; set; }

    [Required(ErrorMessage = "Açıklama zorunludur")]
    [StringLength(500)]
    public string MaterialDescription { get; set; } = string.Empty;

    public string? MaterialImage { get; set; }

    [Required]
    [StringLength(50)]
    public string MaterialCategory { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string MaterialType { get; set; } = string.Empty;

    [StringLength(50)]
    public string? MaterialColor { get; set; }

    [StringLength(50)]
    public string? MaterialSize { get; set; }

    [StringLength(50)]
    public string? MaterialBrand { get; set; }

    [StringLength(50)]
    public string? MaterialModel { get; set; }

    [Required]
    public int SupplierId { get; set; }

    public int? AddressId { get; set; }

    [Required]
    public int CategoryId { get; set; }
}