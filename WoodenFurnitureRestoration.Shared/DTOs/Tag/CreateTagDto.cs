using System.ComponentModel.DataAnnotations;

namespace WoodenFurnitureRestoration.Shared.DTOs.Tag;

public class CreateTagDto
{
    [Required(ErrorMessage = "Etiket adı zorunludur")]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;
}