namespace WoodenFurnitureRestoration.Shared.DTOs.OrderDetail;

public class OrderDetailDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int RestorationId { get; set; }
    public string RestorationName { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice => Quantity * UnitPrice;
    public DateTime CreatedAt { get; set; }
}