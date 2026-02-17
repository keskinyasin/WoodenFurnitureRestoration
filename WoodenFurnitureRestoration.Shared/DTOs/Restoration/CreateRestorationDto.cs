using System.ComponentModel.DataAnnotations;

namespace WoodenFurnitureRestoration.Shared.DTOs.Restoration;

public class CreateRestorationDto
{
    [Required(ErrorMessage = "Restorasyon adı zorunludur")]
    [StringLength(100)]
    public string RestorationName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Açıklama zorunludur")]
    [StringLength(500)]
    public string RestorationDescription { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal RestorationPrice { get; set; }

    public string? RestorationImage { get; set; }

    [Required]
    public string RestorationStatus { get; set; } = "Pending";

    [Required]
    public DateTime RestorationDate { get; set; }

    [Required]
    public DateTime RestorationEndDate { get; set; }

    [Required]
    public int CategoryId { get; set; }
}