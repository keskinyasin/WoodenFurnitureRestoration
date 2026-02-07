using System.ComponentModel.DataAnnotations;

namespace WoodenFurnitureRestoration.Shared.DTOs.OrderDetail;

public class CreateOrderDetailDto
{
    [Required]
    public int OrderId { get; set; }

    [Required]
    public int RestorationId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal UnitPrice { get; set; }
}