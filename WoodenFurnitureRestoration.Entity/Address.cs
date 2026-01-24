using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WoodenFurnitureRestoration.Entities
{
    public class Address : IEntity
    {
        public Address() { }

        // ✅ IEntity Properties (Doğru Şekilde)
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // ✅ Address-specific Properties
        [Required(ErrorMessage = "Lütfen adres satırını belirtiniz.")]
        [Display(Name = "Adres Satırı 1")]
        [StringLength(255, ErrorMessage = "Adres satırı 255 karakterden uzun olamaz.")]
        public string AddressLine1 { get; set; } = string.Empty;

        [Display(Name = "Adres Satırı 2")]
        [StringLength(255, ErrorMessage = "Adres satırı 255 karakterden uzun olamaz.")]
        public string? AddressLine2 { get; set; }

        [Required(ErrorMessage = "Lütfen şehri belirtiniz.")]
        [Display(Name = "Şehir")]
        [StringLength(100, ErrorMessage = "Şehir 100 karakterden uzun olamaz.")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen ilçe giriniz.")]
        [Display(Name = "İlçe")]
        [StringLength(100, ErrorMessage = "İlçe 100 karakterden uzun olamaz.")]
        public string District { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen posta kodunu belirtiniz.")]
        [Display(Name = "Posta Kodu")]
        [StringLength(20, ErrorMessage = "Posta kodu 20 karakterden uzun olamaz.")]
        public string PostalCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen ülkeyi belirtiniz.")]
        [Display(Name = "Ülke")]
        [StringLength(100, ErrorMessage = "Ülke 100 karakterden uzun olamaz.")]
        public string Country { get; set; } = string.Empty;

        // ✅ Foreign Keys
        public int? CustomerId { get; set; }
        public int? SupplierId { get; set; }

        // ✅ Navigation Properties
        public virtual Customer? Customer { get; set; }
        public virtual Supplier? Supplier { get; set; }

        // ✅ Collections
        public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public virtual ICollection<SupplierMaterial> SupplierMaterials { get; set; } = new List<SupplierMaterial>();
        public virtual ICollection<Shipping> Shippings { get; set; } = new List<Shipping>();
        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

        // Constructor
        public Address(
            string addressLine1,
            string city,
            string district,
            string postalCode,
            string country,
            int? customerId = null,
            string? addressLine2 = null)
        {
            AddressLine1 = addressLine1 ?? throw new ArgumentNullException(nameof(addressLine1));
            City = city ?? throw new ArgumentNullException(nameof(city));
            District = district ?? throw new ArgumentNullException(nameof(district));
            PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
            Country = country ?? throw new ArgumentNullException(nameof(country));
            AddressLine2 = addressLine2;
            CustomerId = customerId;
        }
    }
}