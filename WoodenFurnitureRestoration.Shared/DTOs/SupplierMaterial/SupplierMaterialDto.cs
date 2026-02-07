namespace WoodenFurnitureRestoration.Shared.DTOs.SupplierMaterial;

public class SupplierMaterialDto
{
    public int Id { get; set; }
    public int MaterialQuantity { get; set; }
    public string MaterialStatus { get; set; } = string.Empty;
    public string MaterialName { get; set; } = string.Empty;
    public decimal MaterialPrice { get; set; }
    public string MaterialDescription { get; set; } = string.Empty;
    public string? MaterialImage { get; set; }
    public string MaterialCategory { get; set; } = string.Empty;
    public string MaterialType { get; set; } = string.Empty;
    public string? MaterialColor { get; set; }
    public string? MaterialSize { get; set; }
    public string? MaterialBrand { get; set; }
    public string? MaterialModel { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public int? AddressId { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}