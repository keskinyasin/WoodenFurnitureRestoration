using System.ComponentModel.DataAnnotations;

namespace WoodenFurnitureRestoration.Shared.DTOs.Supplier;

public class CreateSupplierDto
{
    [Required(ErrorMessage = "Mağaza adı zorunludur")]
    [StringLength(50)]
    public string ShopName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tedarikçi adı zorunludur")]
    [StringLength(50)]
    public string SupplierName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Adres zorunludur")]
    [StringLength(500)]
    public string SupplierAddress { get; set; } = string.Empty;

    [EmailAddress]
    public string? SupplierEmail { get; set; }

    [Required(ErrorMessage = "Telefon zorunludur")]
    [StringLength(20)]
    public string SupplierPhone { get; set; } = string.Empty;
}