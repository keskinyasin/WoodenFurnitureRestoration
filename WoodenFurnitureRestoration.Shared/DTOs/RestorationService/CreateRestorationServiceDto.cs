using System.ComponentModel.DataAnnotations;

namespace WoodenFurnitureRestoration.Shared.DTOs.RestorationService;

public class CreateRestorationServiceDto
{
    [Required(ErrorMessage = "Hizmet adı zorunludur")]
    [StringLength(50)]
    public string RestorationServiceName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Açıklama zorunludur")]
    [StringLength(500)]
    public string RestorationServiceDescription { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal RestorationServicePrice { get; set; }

    public string? RestorationServiceImage { get; set; }

    [Required]
    public DateTime RestorationServiceDate { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    public int RestorationId { get; set; }
}