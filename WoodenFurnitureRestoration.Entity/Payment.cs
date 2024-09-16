using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Entities
{
    public class Payment : IEntity
    {
        public Payment() { } // Parametresiz yapıcı metot

        public int Id { get; set; } // IEntity'den miras alınan Id özelliği

        [Required(ErrorMessage = "Lütfen ödeme tarihi belirtiniz.")]
        [Display(Name = "Ödeme Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "Lütfen ödeme miktarı belirtiniz.")]
        [Display(Name = "Ödeme Miktarı")]
        [Range(1, double.MaxValue, ErrorMessage = "Ödeme miktarı 1'den küçük olamaz.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Ödeme miktarı sadece rakam içerebilir.")]
        public decimal PaymentAmount { get; set; }

        [Required(ErrorMessage = "Lütfen ödeme yöntemi belirtiniz.")]
        [Display(Name = "Ödeme Yöntemi")]
        [StringLength(50, ErrorMessage = "Ödeme yöntemi 50 karakterden uzun olamaz.")]
        public string PaymentMethod { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen ödeme durumu belirtiniz.")]
        [Display(Name = "Ödeme Durumu")]
        [StringLength(50, ErrorMessage = "Ödeme durumu 50 karakterden uzun olamaz.")]
        public string PaymentStatus { get; set; } = string.Empty;

        [Required]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; } = null!;

        [Required]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;

        public int? ShippingId { get; set; }
        public virtual Shipping? Shipping { get; set; }

        [Required]
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; } = null!;

        [Required]
        public int SupplierMaterialId { get; set; }
        public virtual SupplierMaterial SupplierMaterial { get; set; } = null!;

        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        public virtual ICollection<PaymentTag> PaymentTags { get; set; } = new List<PaymentTag>();
        public virtual ICollection<ShippingPayment> ShippingPayments { get; set; } = new List<ShippingPayment>();
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

        // IEntity properties
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // Constructor with null checks
        public Payment(DateTime paymentDate, decimal paymentAmount, string paymentMethod, string paymentStatus, int addressId, int orderId, int supplierId, int supplierMaterialId)
        {
            PaymentDate = paymentDate;
            PaymentAmount = paymentAmount;
            PaymentMethod = paymentMethod ?? throw new ArgumentNullException(nameof(paymentMethod));
            PaymentStatus = paymentStatus ?? throw new ArgumentNullException(nameof(paymentStatus));
            AddressId = addressId;
            OrderId = orderId;
            SupplierId = supplierId;
            SupplierMaterialId = supplierMaterialId;
        }
    }

}

