namespace WoodenFurnitureRestoration.Shared.DTOs.Supplier;

public class SupplierDto
{
    public int Id { get; set; }
    public string ShopName { get; set; } = string.Empty;
    public string SupplierName { get; set; } = string.Empty;
    public string SupplierAddress { get; set; } = string.Empty;
    public string? SupplierEmail { get; set; }
    public string SupplierPhone { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int ProductCount { get; set; }
    public DateTime CreatedAt { get; set; }
}