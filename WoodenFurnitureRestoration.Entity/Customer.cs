using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Entities
{
    public class Customer : IEntity
    {
        public Customer() { } // Parametresiz yapıcı metot

        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen müşteri adını belirtiniz.")]
        [Display(Name = "Müşteri Adı")]
        [StringLength(50, ErrorMessage = "Müşteri adı 50 karakterden uzun olamaz.")]
        public string CustomerFirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen müşteri soyadını belirtiniz.")]
        [Display(Name = "Müşteri Soyadı")]
        [StringLength(50, ErrorMessage = "Müşteri soyadı 50 karakterden uzun olamaz.")]
        public string CustomerLastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen müşteri e-posta adresini belirtiniz.")]
        [Display(Name = "Müşteri E-Posta Adresi")]
        [StringLength(100, ErrorMessage = "Müşteri e-posta adresi 100 karakterden uzun olamaz.")]
        [EmailAddress(ErrorMessage = "Geçersiz e-posta adresi.")]
        public string CustomerEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen müşteri şifresini belirtiniz.")]
        [Display(Name = "Müşteri Şifresi")]
        [StringLength(50, ErrorMessage = "Müşteri şifresi 50 karakterden uzun olamaz.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Şifre en az 8 karakter, en az bir büyük harf, bir küçük harf, bir sayı ve bir özel karakter içermelidir.")]
        public string CustomerPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen müşteri telefon numarasını belirtiniz.")]
        [Display(Name = "Müşteri Telefon Numarası")]
        [StringLength(20, ErrorMessage = "Müşteri telefon numarası 20 karakterden uzun olamaz.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Telefon numarası 10 haneli olmalıdır.")]
        public string CustomerPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen müşteri şehir bilgisini belirtiniz.")]
        [Display(Name = "Müşteri Şehri")]
        [StringLength(50, ErrorMessage = "Müşteri şehri 50 karakterden uzun olamaz.")]
        public string CustomerCity { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen müşteri ülke bilgisini belirtiniz.")]
        [Display(Name = "Müşteri Ülkesi")]
        [StringLength(50, ErrorMessage = "Müşteri ülkesi 50 karakterden uzun olamaz.")]
        public string CustomerCountry { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen müşteri posta kodunu belirtiniz.")]
        [Display(Name = "Müşteri Posta Kodu")]
        [StringLength(10, ErrorMessage = "Müşteri posta kodu 10 karakterden uzun olamaz.")]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Posta kodu 5 haneli olmalıdır.")]
        public string CustomerPostalCode { get; set; } = string.Empty;

        [Display(Name = "Müşteri Resmi")]
        [DataType(DataType.ImageUrl, ErrorMessage = "Geçersiz görsel URL'si.")]
        public string? CustomerImage { get; set; }

        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        // IEntity properties
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // Constructor with null checks
        public Customer(string customerFirstName, string customerLastName, string customerEmail, string customerPassword, string customerPhone, string customerCity, string customerCountry, string customerPostalCode)
        {
            CustomerFirstName = customerFirstName ?? throw new ArgumentNullException(nameof(customerFirstName));
            CustomerLastName = customerLastName ?? throw new ArgumentNullException(nameof(customerLastName));
            CustomerEmail = customerEmail ?? throw new ArgumentNullException(nameof(customerEmail));
            CustomerPassword = customerPassword ?? throw new ArgumentNullException(nameof(customerPassword));
            CustomerPhone = customerPhone ?? throw new ArgumentNullException(nameof(customerPhone));
            CustomerCity = customerCity ?? throw new ArgumentNullException(nameof(customerCity));
            CustomerCountry = customerCountry ?? throw new ArgumentNullException(nameof(customerCountry));
            CustomerPostalCode = customerPostalCode ?? throw new ArgumentNullException(nameof(customerPostalCode));
        }
    }

}
