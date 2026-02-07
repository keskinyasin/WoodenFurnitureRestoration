namespace WoodenFurnitureRestoration.Shared.DTOs.Restoration;

public class RestorationDto
{
    public int Id { get; set; }
    public string RestorationName { get; set; } = string.Empty;
    public string RestorationDescription { get; set; } = string.Empty;
    public decimal RestorationPrice { get; set; }
    public string RestorationImage { get; set; } = string.Empty;
    public string RestorationStatus { get; set; } = string.Empty;
    public DateTime RestorationDate { get; set; }
    public DateTime RestorationEndDate { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}