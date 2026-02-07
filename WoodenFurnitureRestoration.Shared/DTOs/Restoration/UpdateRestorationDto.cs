namespace WoodenFurnitureRestoration.Shared.DTOs.Restoration;

public class UpdateRestorationDto
{
    public string? RestorationName { get; set; }
    public string? RestorationDescription { get; set; }
    public decimal? RestorationPrice { get; set; }
    public string? RestorationImage { get; set; }
    public string? RestorationStatus { get; set; }
    public DateTime? RestorationDate { get; set; }
    public DateTime? RestorationEndDate { get; set; }
    public int? CategoryId { get; set; }
}