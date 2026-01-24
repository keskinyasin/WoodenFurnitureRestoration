using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WoodenFurnitureRestoration.Entities
{
    public class Order : IEntity
    {
        public Order() { }

        // ✅ IEntity Properties (Basit Auto-Properties)
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // ✅ Order-Specific Properties
        [Required(ErrorMessage = "Lütfen sipariş tarihi belirtiniz.")]
        [Display(Name = "Sipariş Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Lütfen sipariş durumu belirtiniz.")]
        [Display(Name = "Sipariş Durumu")]
        [StringLength(50, ErrorMessage = "Sipariş durumu 50 karakterden uzun olamaz.")]
        public string OrderStatus { get; set; } = string.Empty;

        // ✅ Foreign Keys
        [Required]
        public int CustomerId { get; set; }

        public int? ShippingId { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Required]
        public int SupplierMaterialId { get; set; }

        // ✅ Navigation Properties
        public virtual Customer Customer { get; set; } = null!;
        public virtual Shipping? Shipping { get; set; }
        public virtual Supplier Supplier { get; set; } = null!;
        public virtual SupplierMaterial SupplierMaterial { get; set; } = null!;

        // ✅ Collections (Circular Reference Önleme)
        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        [JsonIgnore]
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

        [JsonIgnore]
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

        [JsonIgnore]
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

        [JsonIgnore]
        public virtual ICollection<OrderTag> OrderTags { get; set; } = new List<OrderTag>();

        // Constructor
        public Order(
            DateTime orderDate,
            string orderStatus,
            int customerId,
            int supplierId,
            int supplierMaterialId,
            int? shippingId = null)
        {
            OrderDate = orderDate;
            OrderStatus = orderStatus ?? throw new ArgumentNullException(nameof(orderStatus));
            CustomerId = customerId;
            SupplierId = supplierId;
            SupplierMaterialId = supplierMaterialId;
            ShippingId = shippingId;
        }
    }
}