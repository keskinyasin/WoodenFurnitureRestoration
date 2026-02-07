using System.ComponentModel.DataAnnotations;

namespace WoodenFurnitureRestoration.Shared.DTOs.Address;

public class CreateAddressDto
{
    [Required(ErrorMessage = "Adres satırı 1 zorunludur")]
    [StringLength(255)]
    public string AddressLine1 { get; set; } = string.Empty;

    [StringLength(255)]
    public string? AddressLine2 { get; set; }

    [Required(ErrorMessage = "Şehir zorunludur")]
    [StringLength(100)]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "İlçe zorunludur")]
    [StringLength(100)]
    public string District { get; set; } = string.Empty;

    [Required(ErrorMessage = "Posta kodu zorunludur")]
    [StringLength(20)]
    public string PostalCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ülke zorunludur")]
    [StringLength(100)]
    public string Country { get; set; } = string.Empty;

    public int? CustomerId { get; set; }
    public int? SupplierId { get; set; }
}