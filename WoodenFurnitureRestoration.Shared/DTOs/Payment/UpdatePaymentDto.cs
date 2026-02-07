namespace WoodenFurnitureRestoration.Shared.DTOs.Payment;

public class UpdatePaymentDto
{
    public decimal? PaymentAmount { get; set; }
    public string? PaymentMethod { get; set; }
    public string? PaymentStatus { get; set; }
}