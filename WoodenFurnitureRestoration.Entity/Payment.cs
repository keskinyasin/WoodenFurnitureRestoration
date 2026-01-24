using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WoodenFurnitureRestoration.Entities
{
    public class Payment : IEntity
    {
        public Payment() { }

        // ✅ IEntity Properties (Basit Auto-Properties)
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // ✅ Payment-Specific Properties
        [Required(ErrorMessage = "Lütfen ödeme tarihi belirtiniz.")]
        [Display(Name = "Ödeme Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "Lütfen ödeme miktarı belirtiniz.")]
        [Display(Name = "Ödeme Miktarı")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Ödeme miktarı 0'dan büyük olmalıdır.")]
        [DataType(DataType.Currency)]
        public decimal PaymentAmount { get; set; }

        [Required(ErrorMessage = "Lütfen ödeme yöntemi belirtiniz.")]
        [Display(Name = "Ödeme Yöntemi")]
        [StringLength(50, ErrorMessage = "Ödeme yöntemi 50 karakterden uzun olamaz.")]
        public string PaymentMethod { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen ödeme durumu belirtiniz.")]
        [Display(Name = "Ödeme Durumu")]
        [StringLength(50, ErrorMessage = "Ödeme durumu 50 karakterden uzun olamaz.")]
        public string PaymentStatus { get; set; } = string.Empty;

        // ✅ Foreign Keys
        [Required]
        public int AddressId { get; set; }

        [Required]
        public int OrderId { get; set; }

        public int? ShippingId { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Required]
        public int SupplierMaterialId { get; set; }

        // ✅ Navigation Properties
        [JsonIgnore]
        public virtual Address Address { get; set; } = null!;

        [JsonIgnore]
        public virtual Order Order { get; set; } = null!;

        [JsonIgnore]
        public virtual Shipping? Shipping { get; set; }

        [JsonIgnore]
        public virtual Supplier Supplier { get; set; } = null!;

        [JsonIgnore]
        public virtual SupplierMaterial SupplierMaterial { get; set; } = null!;

        // ✅ Collections (Circular Reference Önleme)
        [JsonIgnore]
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

        [JsonIgnore]
        public virtual ICollection<PaymentTag> PaymentTags { get; set; } = new List<PaymentTag>();

        [JsonIgnore]
        public virtual ICollection<ShippingPayment> ShippingPayments { get; set; } = new List<ShippingPayment>();

        [JsonIgnore]
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

        // Constructor
        public Payment(
            DateTime paymentDate,
            decimal paymentAmount,
            string paymentMethod,
            string paymentStatus,
            int addressId,
            int orderId,
            int supplierId,
            int supplierMaterialId,
            int? shippingId = null)
        {
            PaymentDate = paymentDate;
            PaymentAmount = paymentAmount;
            PaymentMethod = paymentMethod ?? throw new ArgumentNullException(nameof(paymentMethod));
            PaymentStatus = paymentStatus ?? throw new ArgumentNullException(nameof(paymentStatus));
            AddressId = addressId;
            OrderId = orderId;
            SupplierId = supplierId;
            SupplierMaterialId = supplierMaterialId;
            ShippingId = shippingId;
        }
    }
}