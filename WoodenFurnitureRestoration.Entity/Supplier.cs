using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int Id { get; set; }

        [Display(Name = "Mağaza Adı")]
        [Required(ErrorMessage = "Mağaza adı boş bırakılamaz.")]
        [StringLength(50, ErrorMessage = "Mağaza adı 50 karakterden uzun olamaz.")]
        public string ShopName { get; set; } = string.Empty;

        [Display(Name = "Tedarikçi Adı")]
        [Required(ErrorMessage = "Tedarikçi adı boş bırakılamaz.")]
        [StringLength(50, ErrorMessage = "Tedarikçi adı 50 karakterden uzun olamaz.")]
        public string SupplierName { get; set; } = string.Empty;

        [Display(Name = "Tedarikçi Adresi")]
        [Required(ErrorMessage = "Tedarikçi adresi boş bırakılamaz.")]
        [StringLength(500, ErrorMessage = "Tedarikçi adresi 500 karakterden uzun olamaz.")]
        public string SupplierAddress { get; set; } = string.Empty;

        [Display(Name = "Tedarikçi E-posta")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string SupplierEmail { get; set; } = string.Empty;

        [Display(Name = "Tedarikçi Telefon")]
        [Required(ErrorMessage = "Tedarikçi telefonu boş bırakılamaz.")]
        public string SupplierPhone { get; set; } = string.Empty;

        [Display(Name = "Tedarikçi Durumu")]
        [Required(ErrorMessage = "Tedarikçi durumu boş bırakılamaz.")]
        public SupplierStatus Status { get; set; }  // Enum kullanımı

        public ICollection<Address> Addresses { get; set; } = new List<Address>();

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<SupplierMaterial> SupplierMaterials { get; set; } = new List<SupplierMaterial>();
        public virtual ICollection<Shipping> Shippings { get; set; } = new List<Shipping>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
        public virtual ICollection<SupplierCategory> SupplierCategories { get; set; } = new List<SupplierCategory>();

        // IEntity ile uyumlu olmak için gerekli ek özellikler
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // Constructor with null checks
        public Supplier(string shopName, string supplierName, string supplierAddress, string supplierEmail, string supplierPhone, SupplierStatus status)
        {
            ShopName = shopName ?? throw new ArgumentNullException(nameof(shopName));
            SupplierName = supplierName ?? throw new ArgumentNullException(nameof(supplierName));
            SupplierAddress = supplierAddress ?? throw new ArgumentNullException(nameof(supplierAddress));
            SupplierEmail = supplierEmail ?? throw new ArgumentNullException(nameof(supplierEmail));
            SupplierPhone = supplierPhone ?? throw new ArgumentNullException(nameof(supplierPhone));
            Status = status;
        }
    }
}
