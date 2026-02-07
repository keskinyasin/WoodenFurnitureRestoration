namespace WoodenFurnitureRestoration.Shared.DTOs.Review;

public class ReviewDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public int SupplierMaterialId { get; set; }
    public int RestorationId { get; set; }
    public string RestorationName { get; set; } = string.Empty;
    public string ReviewDescription { get; set; } = string.Empty;
    public DateTime ReviewDate { get; set; }
    public int Rating { get; set; }
    public string ReviewStatus { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}