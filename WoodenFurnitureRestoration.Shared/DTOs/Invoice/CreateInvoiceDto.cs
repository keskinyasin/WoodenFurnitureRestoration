using System.ComponentModel.DataAnnotations;

namespace WoodenFurnitureRestoration.Shared.DTOs.Invoice;

public class CreateInvoiceDto
{
    [Required]
    public DateTime InvoiceDate { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal TotalAmount { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Discount { get; set; }

    [Required]
    public int OrderId { get; set; }

    public int? PaymentId { get; set; }

    [Required]
    public int SupplierMaterialId { get; set; }

    public int? ShippingId { get; set; }

    [Required]
    public int SupplierId { get; set; }
}