using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Entities
{
    public class Invoice : IEntity
    {
        public Invoice() { } // Parametresiz yapıcı metot

        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen fatura tarihi belirtiniz.")]
        [Display(Name = "Fatura Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }

        [Required(ErrorMessage = "Lütfen toplam miktarı belirtiniz.")]
        [Display(Name = "Toplam Miktar")]
        [Range(1, double.MaxValue, ErrorMessage = "Toplam miktar 1'den küçük olamaz.")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Lütfen indirimi belirtiniz.")]
        [Display(Name = "İndirim")]
        [Range(0, double.MaxValue, ErrorMessage = "İndirim 0'dan küçük olamaz.")]
        public decimal Discount { get; set; }

        [Required(ErrorMessage = "Lütfen net miktarı belirtiniz.")]
        [Display(Name = "Net Miktar")]
        [Range(1, double.MaxValue, ErrorMessage = "Net miktar 1'den küçük olamaz.")]
        public decimal NetAmount { get; set; }

        // Relationships
        [Required]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;

        public int? PaymentId { get; set; }
        public virtual Payment? Payment { get; set; }

        [Required]
        public int SupplierMaterialId { get; set; }
        public virtual SupplierMaterial SupplierMaterial { get; set; } = null!;

        public int? ShippingId { get; set; }
        public virtual Shipping? Shipping { get; set; }

        [Required]
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; } = null!;

        public virtual ICollection<InvoiceTag> InvoiceTags { get; set; } = new List<InvoiceTag>();
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public virtual ICollection<Shipping> Shippings { get; set; } = new List<Shipping>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

        // IEntity properties
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // Constructor with null checks
        public Invoice(DateTime invoiceDate, decimal totalAmount, decimal discount, decimal netAmount, int orderId, int supplierMaterialId, int supplierId)
        {
            InvoiceDate = invoiceDate;
            TotalAmount = totalAmount;
            Discount = discount;
            NetAmount = netAmount;
            OrderId = orderId;
            SupplierMaterialId = supplierMaterialId;
            SupplierId = supplierId;
        }
    }

}

