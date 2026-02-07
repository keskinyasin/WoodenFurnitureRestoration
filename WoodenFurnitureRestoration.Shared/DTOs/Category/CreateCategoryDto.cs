using System.ComponentModel.DataAnnotations;

namespace WoodenFurnitureRestoration.Shared.DTOs.Category;

public class CreateCategoryDto
{
    [Required(ErrorMessage = "Kategori adı zorunludur")]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    // Opsiyonel yap (nullable)
    public int? SupplierId { get; set; }
}