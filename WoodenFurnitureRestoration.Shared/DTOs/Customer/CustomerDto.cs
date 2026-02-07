namespace WoodenFurnitureRestoration.Shared.DTOs.Customer;

public class CustomerDto
{
    public int Id { get; set; }
    public string CustomerFirstName { get; set; } = string.Empty;
    public string CustomerLastName { get; set; } = string.Empty;
    public string FullName => $"{CustomerFirstName} {CustomerLastName}";
    public string CustomerEmail { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public string CustomerCity { get; set; } = string.Empty;
    public string CustomerCountry { get; set; } = string.Empty;
    public string CustomerPostalCode { get; set; } = string.Empty;
    public string? CustomerImage { get; set; }
    public DateTime CreatedAt { get; set; }
}