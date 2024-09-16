using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Entities
{

    public class Address : IEntity
    {
        public Address() { } // Parametresiz yapıcı eklendi

        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen adres satırını belirtiniz.")]
        [Display(Name = "Adres Satırı 1")]
        [StringLength(255, ErrorMessage = "Adres satırı 255 karakterden uzun olamaz.")]
        public string AddressLine1 { get; set; } // 'required' anahtar kelimesi eklendi

        [Display(Name = "Adres Satırı 2")]
        [StringLength(255, ErrorMessage = "Adres satırı 255 karakterden uzun olamaz.")]
        public string? AddressLine2 { get; set; } // Nullable olarak işaretlendi

        [Required(ErrorMessage = "Lütfen şehri belirtiniz.")]
        [Display(Name = "Şehir")]
        [StringLength(100, ErrorMessage = "Şehir 100 karakterden uzun olamaz.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Lütfen İlçe Giriniz.")]
        [Display(Name = "İlçe")]
        [StringLength(100, ErrorMessage = "İlçe 100 karakterden uzun olamaz.")]
        public string District { get; set; }

        [Required(ErrorMessage = "Lütfen posta kodunu belirtiniz.")]
        [Display(Name = "Posta Kodu")]
        [StringLength(20, ErrorMessage = "Posta kodu 20 karakterden uzun olamaz.")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Lütfen ülkeyi belirtiniz.")]
        [Display(Name = "Ülke")]
        [StringLength(100, ErrorMessage = "Ülke 100 karakterden uzun olamaz.")]
        public string Country { get; set; }

        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        public int? SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public virtual ICollection<SupplierMaterial> SupplierMaterials { get; set; } = new List<SupplierMaterial>();
        public virtual ICollection<Shipping> Shippings { get; set; } = new List<Shipping>();
        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

        // IEntity properties
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // Constructor with null checks
        public Address(string addressLine1, string city, string district, string postalCode, string country)
        {
            AddressLine1 = addressLine1 ?? throw new ArgumentNullException(nameof(addressLine1));
            City = city ?? throw new ArgumentNullException(nameof(city));
            District = district ?? throw new ArgumentNullException(nameof(district));
            PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
            Country = country ?? throw new ArgumentNullException(nameof(country));
        }
    }




}

