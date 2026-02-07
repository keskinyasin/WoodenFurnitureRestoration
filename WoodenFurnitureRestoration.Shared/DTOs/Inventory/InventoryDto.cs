namespace WoodenFurnitureRestoration.Shared.DTOs.Inventory;

public class InventoryDto
{
    public int Id { get; set; }
    public int QuantityInStock { get; set; }
    public DateTime LastUpdate { get; set; }
    public decimal Price { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime InventoryDate { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int SupplierMaterialId { get; set; }
    public string SupplierMaterialName { get; set; } = string.Empty;
    public int AddressId { get; set; }
    public DateTime CreatedAt { get; set; }
}