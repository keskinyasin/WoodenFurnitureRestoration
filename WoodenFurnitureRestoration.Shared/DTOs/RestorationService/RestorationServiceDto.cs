namespace WoodenFurnitureRestoration.Shared.DTOs.RestorationService;

public class RestorationServiceDto
{
    public int Id { get; set; }
    public string RestorationServiceName { get; set; } = string.Empty;
    public string RestorationServiceDescription { get; set; } = string.Empty;
    public decimal RestorationServicePrice { get; set; }
    public string? RestorationServiceImage { get; set; }
    public string RestorationServiceStatus { get; set; } = string.Empty;
    public DateTime RestorationServiceDate { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int RestorationId { get; set; }
    public string RestorationName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}