using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WoodenFurnitureRestoration.Entities
{
    public enum ShippingType
    {
        Standard,
        Express,
        Overnight
    }

    public enum ShippingStatus
    {
        Pending,
        Shipped,
        Delivered,
        Cancelled
    }

    public class Shipping : IEntity
    {
        public Shipping() { }

        // ✅ IEntity Properties
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // ✅ Shipping-Specific Properties
        [Required(ErrorMessage = "Lütfen teslimat tarihi belirtiniz.")]
        [Display(Name = "Teslimat Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ShippingDate { get; set; }

        [Required(ErrorMessage = "Lütfen teslimat türü belirtiniz.")]
        [Display(Name = "Teslimat Türü")]
        public ShippingType ShippingType { get; set; }

        [Required(ErrorMessage = "Lütfen teslimat ücreti belirtiniz.")]
        [Display(Name = "Teslimat Ücreti")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Teslimat ücreti 0'dan büyük olmalıdır.")]
        [DataType(DataType.Currency)]
        public decimal ShippingCost { get; set; }

        [Required(ErrorMessage = "Lütfen teslimat durumunu belirtiniz.")]
        [Display(Name = "Teslimat Durumu")]
        public ShippingStatus ShippingStatus { get; set; }

        // ✅ Foreign Keys (SADECE GEREK OLANLAR)
        [Required(ErrorMessage = "Lütfen sipariş numarası belirtiniz.")]
        [Display(Name = "Sipariş Numarası")]
        [Range(1, int.MaxValue, ErrorMessage = "Sipariş numarası 1'den küçük olamaz.")]
        public int OrderId { get; set; }

        [Required]
        public int AddressId { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Required]
        public int SupplierMaterialId { get; set; }

        // ✅ Navigation Properties (SADECE GEREK OLANLAR)
        [JsonIgnore]
        public virtual Order Order { get; set; } = null!;

        [JsonIgnore]
        public virtual Address Address { get; set; } = null!;

        [JsonIgnore]
        public virtual Supplier Supplier { get; set; } = null!;

        [JsonIgnore]
        public virtual SupplierMaterial SupplierMaterial { get; set; } = null!;

        // ✅ Collections (Join Tables SADECE)
        [JsonIgnore]
        public virtual ICollection<ShippingPayment> ShippingPayments { get; set; } = new List<ShippingPayment>();

        [JsonIgnore]
        public virtual ICollection<ShippingProduct> ShippingProducts { get; set; } = new List<ShippingProduct>();

        [JsonIgnore]
        public virtual ICollection<ShippingInventory> ShippingInventories { get; set; } = new List<ShippingInventory>();

        [JsonIgnore]
        public virtual ICollection<ShippingTag> ShippingTags { get; set; } = new List<ShippingTag>();

        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        [JsonIgnore]
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
        [JsonIgnore]
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

        // Constructor
        public Shipping(
            DateTime shippingDate,
            ShippingType shippingType,
            decimal shippingCost,
            ShippingStatus shippingStatus,
            int orderId,
            int addressId,
            int supplierId,
            int supplierMaterialId)
        {
            ShippingDate = shippingDate;
            ShippingType = shippingType;
            ShippingCost = shippingCost;
            ShippingStatus = shippingStatus;
            OrderId = orderId;
            AddressId = addressId;
            SupplierId = supplierId;
            SupplierMaterialId = supplierMaterialId;
        }
    }
}