using System.ComponentModel.DataAnnotations;

namespace WoodenFurnitureRestoration.Shared.DTOs.Inventory;

public class CreateInventoryDto
{
    [Required]
    [Range(0, int.MaxValue)]
    public int QuantityInStock { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    public DateTime InventoryDate { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public int SupplierMaterialId { get; set; }

    [Required]
    public int AddressId { get; set; }
}