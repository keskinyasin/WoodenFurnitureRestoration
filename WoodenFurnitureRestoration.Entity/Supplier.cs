using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WoodenFurnitureRestoration.Entities
{
    public enum SupplierStatus
    {
        Active,
        Inactive,
        Pending
    }

    public class Supplier : IEntity
    {
        public Supplier() { }

        // ✅ IEntity Properties (Basit Auto-Properties)
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // ✅ Supplier-Specific Properties
        [Required(ErrorMessage = "Lütfen mağaza adını belirtiniz.")]
        [Display(Name = "Mağaza Adı")]
        [StringLength(50, ErrorMessage = "Mağaza adı 50 karakterden uzun olamaz.")]
        public string ShopName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen tedarikçi adını belirtiniz.")]
        [Display(Name = "Tedarikçi Adı")]
        [StringLength(50, ErrorMessage = "Tedarikçi adı 50 karakterden uzun olamaz.")]
        public string SupplierName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen tedarikçi adresini belirtiniz.")]
        [Display(Name = "Tedarikçi Adresi")]
        [StringLength(500, ErrorMessage = "Tedarikçi adresi 500 karakterden uzun olamaz.")]
        public string SupplierAddress { get; set; } = string.Empty;

        [Display(Name = "Tedarikçi E-Posta")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string? SupplierEmail { get; set; }

        [Required(ErrorMessage = "Lütfen tedarikçi telefon numarasını belirtiniz.")]
        [Display(Name = "Tedarikçi Telefon")]
        [StringLength(20, ErrorMessage = "Telefon numarası 20 karakterden uzun olamaz.")]
        [DataType(DataType.PhoneNumber)]
        public string SupplierPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen tedarikçi durumunu belirtiniz.")]
        [Display(Name = "Tedarikçi Durumu")]
        public SupplierStatus Status { get; set; }

        // ✅ Collections (Circular Reference Önleme)
        [JsonIgnore]
        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        [JsonIgnore]
        public virtual ICollection<SupplierMaterial> SupplierMaterials { get; set; } = new List<SupplierMaterial>();

        [JsonIgnore]
        public virtual ICollection<Shipping> Shippings { get; set; } = new List<Shipping>();

        [JsonIgnore]
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        [JsonIgnore]
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        [JsonIgnore]
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

        [JsonIgnore]
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

        [JsonIgnore]
        public virtual ICollection<SupplierCategory> SupplierCategories { get; set; } = new List<SupplierCategory>();

        // Constructor
        public Supplier(
            string shopName,
            string supplierName,
            string supplierAddress,
            string supplierPhone,
            SupplierStatus status,
            string? supplierEmail = null)
        {
            ShopName = shopName ?? throw new ArgumentNullException(nameof(shopName));
            SupplierName = supplierName ?? throw new ArgumentNullException(nameof(supplierName));
            SupplierAddress = supplierAddress ?? throw new ArgumentNullException(nameof(supplierAddress));
            SupplierPhone = supplierPhone ?? throw new ArgumentNullException(nameof(supplierPhone));
            Status = status;
            SupplierEmail = supplierEmail;
        }
    }
}