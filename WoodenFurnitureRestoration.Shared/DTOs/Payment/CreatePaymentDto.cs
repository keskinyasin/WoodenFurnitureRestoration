using System.ComponentModel.DataAnnotations;

namespace WoodenFurnitureRestoration.Shared.DTOs.Payment;

public class CreatePaymentDto
{
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal PaymentAmount { get; set; }

    [Required]
    [StringLength(50)]
    public string PaymentMethod { get; set; } = string.Empty;

    [Required]
    public int AddressId { get; set; }

    [Required]
    public int OrderId { get; set; }

    public int? ShippingId { get; set; }

    [Required]
    public int SupplierId { get; set; }

    [Required]
    public int SupplierMaterialId { get; set; }
}