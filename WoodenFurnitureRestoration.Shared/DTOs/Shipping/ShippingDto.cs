namespace WoodenFurnitureRestoration.Shared.DTOs.Shipping;

public class ShippingDto
{
    public int Id { get; set; }
    public DateTime ShippingDate { get; set; }
    public string ShippingType { get; set; } = string.Empty;
    public decimal ShippingCost { get; set; }
    public string ShippingStatus { get; set; } = string.Empty;
    public int OrderId { get; set; }
    public int AddressId { get; set; }
    public string AddressCity { get; set; } = string.Empty;
    public int SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public int SupplierMaterialId { get; set; }
    public DateTime CreatedAt { get; set; }
}