namespace WoodenFurnitureRestoration.Shared.DTOs.RestorationService;

public class UpdateRestorationServiceDto
{
    public string? RestorationServiceName { get; set; }
    public string? RestorationServiceDescription { get; set; }
    public decimal? RestorationServicePrice { get; set; }
    public string? RestorationServiceImage { get; set; }
    public string? RestorationServiceStatus { get; set; }
    public DateTime? RestorationServiceDate { get; set; }
    public int? CategoryId { get; set; }
}