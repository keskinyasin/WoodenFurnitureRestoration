using System.ComponentModel.DataAnnotations;

namespace WoodenFurnitureRestoration.Shared.DTOs.Shipping;

public class CreateShippingDto
{
    [Required]
    public DateTime ShippingDate { get; set; }

    [Required]
    [StringLength(50)]
    public string ShippingType { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal ShippingCost { get; set; }

    [Required]
    public int OrderId { get; set; }

    [Required]
    public int AddressId { get; set; }

    [Required]
    public int SupplierId { get; set; }

    [Required]
    public int SupplierMaterialId { get; set; }
}