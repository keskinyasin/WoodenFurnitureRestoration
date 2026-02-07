namespace WoodenFurnitureRestoration.Shared.DTOs.Payment;

public class PaymentDto
{
    public int Id { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal PaymentAmount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public int AddressId { get; set; }
    public int OrderId { get; set; }
    public int? ShippingId { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public int SupplierMaterialId { get; set; }
    public DateTime CreatedAt { get; set; }
}