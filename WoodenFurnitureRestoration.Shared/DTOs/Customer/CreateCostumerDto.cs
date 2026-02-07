using System.ComponentModel.DataAnnotations;

namespace WoodenFurnitureRestoration.Shared.DTOs.Customer;

public class CreateCustomerDto
{
    [Required(ErrorMessage = "Ad zorunludur")]
    [StringLength(50)]
    public string CustomerFirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Soyad zorunludur")]
    [StringLength(50)]
    public string CustomerLastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-posta zorunludur")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
    [StringLength(100)]
    public string CustomerEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre zorunludur")]
    [StringLength(50, MinimumLength = 6)]
    public string CustomerPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Telefon zorunludur")]
    [StringLength(20)]
    public string CustomerPhone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şehir zorunludur")]
    [StringLength(50)]
    public string CustomerCity { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ülke zorunludur")]
    [StringLength(50)]
    public string CustomerCountry { get; set; } = string.Empty;

    [Required(ErrorMessage = "Posta kodu zorunludur")]
    [StringLength(10)]
    public string CustomerPostalCode { get; set; } = string.Empty;

    public string? CustomerImage { get; set; }
}