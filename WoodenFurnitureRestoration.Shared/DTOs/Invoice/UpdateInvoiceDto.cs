namespace WoodenFurnitureRestoration.Shared.DTOs.Invoice;

public class UpdateInvoiceDto
{
    public DateTime? InvoiceDate { get; set; }
    public decimal? TotalAmount { get; set; }
    public decimal? Discount { get; set; }
    public int? PaymentId { get; set; }
    public int? ShippingId { get; set; }
}