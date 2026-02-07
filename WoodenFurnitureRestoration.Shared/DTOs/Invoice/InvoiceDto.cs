namespace WoodenFurnitureRestoration.Shared.DTOs.Invoice;

public class InvoiceDto
{
    public int Id { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal Discount { get; set; }
    public decimal NetAmount { get; set; }
    public int OrderId { get; set; }
    public int? PaymentId { get; set; }
    public int SupplierMaterialId { get; set; }
    public int? ShippingId { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}