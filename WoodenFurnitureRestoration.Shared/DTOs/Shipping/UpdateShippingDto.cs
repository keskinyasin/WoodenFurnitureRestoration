namespace WoodenFurnitureRestoration.Shared.DTOs.Shipping;

public class UpdateShippingDto
{
    public DateTime? ShippingDate { get; set; }
    public string? ShippingType { get; set; }
    public decimal? ShippingCost { get; set; }
    public string? ShippingStatus { get; set; }
}