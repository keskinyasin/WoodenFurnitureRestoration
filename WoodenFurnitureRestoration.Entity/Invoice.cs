using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WoodenFurnitureRestoration.Entities
{
    public class Invoice : IEntity
    {
        public Invoice() { }

        // ✅ IEntity Properties (Basit Auto-Properties)
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // ✅ Invoice-Specific Properties
        [Required(ErrorMessage = "Lütfen fatura tarihi belirtiniz.")]
        [Display(Name = "Fatura Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }

        [Required(ErrorMessage = "Lütfen toplam miktarı belirtiniz.")]
        [Display(Name = "Toplam Miktar")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Toplam miktar 0'dan büyük olmalıdır.")]
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Lütfen indirimi belirtiniz.")]
        [Display(Name = "İndirim")]
        [Range(0, double.MaxValue, ErrorMessage = "İndirim 0'dan küçük olamaz.")]
        [DataType(DataType.Currency)]
        public decimal Discount { get; set; }

        [Required(ErrorMessage = "Lütfen net miktarı belirtiniz.")]
        [Display(Name = "Net Miktar")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Net miktar 0'dan büyük olmalıdır.")]
        [DataType(DataType.Currency)]
        public decimal NetAmount { get; set; }

        // ✅ Foreign Keys
        [Required]
        public int OrderId { get; set; }

        public int? PaymentId { get; set; }

        [Required]
        public int SupplierMaterialId { get; set; }

        public int? ShippingId { get; set; }

        [Required]
        public int SupplierId { get; set; }

        // ✅ Navigation Properties
        public virtual Order Order { get; set; } = null!;
        public virtual Payment? Payment { get; set; }
        public virtual SupplierMaterial SupplierMaterial { get; set; } = null!;
        public virtual Shipping? Shipping { get; set; }
        public virtual Supplier Supplier { get; set; } = null!;

        // ✅ Collections (Circular Reference Önleme)
        [JsonIgnore]
        public virtual ICollection<InvoiceTag> InvoiceTags { get; set; } = new List<InvoiceTag>();

        [JsonIgnore]
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

        // Constructor
        public Invoice(
            DateTime invoiceDate,
            decimal totalAmount,
            decimal discount,
            decimal netAmount,
            int orderId,
            int supplierMaterialId,
            int supplierId,
            int? paymentId = null,
            int? shippingId = null)
        {
            InvoiceDate = invoiceDate;
            TotalAmount = totalAmount;
            Discount = discount;
            NetAmount = netAmount;
            OrderId = orderId;
            SupplierMaterialId = supplierMaterialId;
            SupplierId = supplierId;
            PaymentId = paymentId;
            ShippingId = shippingId;
        }
    }
}