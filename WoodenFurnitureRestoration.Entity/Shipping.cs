using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

        public int Id { get; set; } // IEntity'den gelen Id özelliği

        [Required(ErrorMessage = "Lütfen teslimat tarihi belirtiniz.")]
        [Display(Name = "Teslimat Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ShippingDate { get; set; }

        [Required(ErrorMessage = "Lütfen teslimat türü belirtiniz.")]
        [Display(Name = "Teslimat Türü")]
        public ShippingType ShippingType { get; set; }  // Enum ile belirlenen teslimat türü

        [Required(ErrorMessage = "Lütfen teslimat ücreti belirtiniz.")]
        [Display(Name = "Teslimat Ücreti")]
        [Range(1, int.MaxValue, ErrorMessage = "Teslimat ücreti 1'den küçük olamaz.")]
        public decimal ShippingCost { get; set; }

        [Required(ErrorMessage = "Lütfen teslimat durumu belirtiniz.")]
        [Display(Name = "Teslimat Durumu")]
        public ShippingStatus ShippingStatus { get; set; }  // Enum ile belirlenen teslimat durumu

        [Required(ErrorMessage = "Lütfen sipariş numarası belirtiniz.")]
        [Display(Name = "Sipariş Numarası")]
        [Range(1, int.MaxValue, ErrorMessage = "Sipariş numarası 1'den küçük olamaz.")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public int AddressId { get; set; }
        public virtual Address Address { get; set; } = null!;

        public int? InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; } = null!;

        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; } = null!;

        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;

        public int SupplierMaterialId { get; set; }
        public virtual SupplierMaterial SupplierMaterial { get; set; } = null!;

        public int InventoryId { get; set; }
        public virtual Inventory Inventory { get; set; } = null!;

        public virtual ICollection<ShippingPayment> ShippingPayments { get; set; } = new List<ShippingPayment>();
        public virtual ICollection<SupplierMaterial> SupplierMaterials { get; set; } = new List<SupplierMaterial>();
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public virtual ICollection<ShippingTag> ShippingTags { get; set; } = new List<ShippingTag>();

        public virtual ICollection<ShippingProduct> ShippingProducts { get; set; } = new List<ShippingProduct>();

        public virtual ICollection<ShippingInventory> ShippingInventories { get; set; } = new List<ShippingInventory>();

        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

        // IEntity ile uyumlu olmak için gerekli ek özellikler
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // Constructor with null checks
        public Shipping(DateTime? shippingDate, ShippingType shippingType, decimal shippingCost, ShippingStatus shippingStatus, int orderId, int addressId, int supplierId, int productId, int supplierMaterialId, int inventoryId)
        {
            ShippingDate = shippingDate ?? throw new ArgumentNullException(nameof(shippingDate));
            ShippingType = shippingType;
            ShippingCost = shippingCost;
            ShippingStatus = shippingStatus;
            OrderId = orderId;
            AddressId = addressId;
            SupplierId = supplierId;
            ProductId = productId;
            SupplierMaterialId = supplierMaterialId;
            InventoryId = inventoryId;
        }
    }
}

