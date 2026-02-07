namespace WoodenFurnitureRestoration.Shared.DTOs.SupplierMaterial;

public class UpdateSupplierMaterialDto
{
    public int? MaterialQuantity { get; set; }
    public string? MaterialStatus { get; set; }
    public string? MaterialName { get; set; }
    public decimal? MaterialPrice { get; set; }
    public string? MaterialDescription { get; set; }
    public string? MaterialImage { get; set; }
    public string? MaterialCategory { get; set; }
    public string? MaterialType { get; set; }
    public string? MaterialColor { get; set; }
    public string? MaterialSize { get; set; }
    public string? MaterialBrand { get; set; }
    public string? MaterialModel { get; set; }
}