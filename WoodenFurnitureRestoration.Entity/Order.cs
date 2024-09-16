using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Entities
{
    public class Order : IEntity
    {
        public Order() { } // Parametresiz yapıcı metot

        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen sipariş tarihi belirtiniz.")]
        [Display(Name = "Sipariş Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Lütfen sipariş durumu belirtiniz.")]
        [Display(Name = "Sipariş Durumu")]
        [StringLength(50, ErrorMessage = "Sipariş durumu 50 karakterden uzun olamaz.")]
        public string OrderStatus { get; set; } = string.Empty;

        [Required]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = null!;

        public int? ShippingId { get; set; }
        public virtual Shipping? Shipping { get; set; }

        [Required]
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; } = null!;

        [Required]
        public int SupplierMaterialId { get; set; }
        public virtual SupplierMaterial SupplierMaterial { get; set; } = null!;

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public virtual ICollection<Shipping> Shippings { get; set; } = new List<Shipping>();
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public virtual ICollection<OrderTag> OrderTags { get; set; } = new List<OrderTag>();

        // IEntity properties
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // Constructor with null checks
        public Order(DateTime orderDate, string orderStatus, int customerId, int supplierId, int supplierMaterialId)
        {
            OrderDate = orderDate;
            OrderStatus = orderStatus ?? throw new ArgumentNullException(nameof(orderStatus));
            CustomerId = customerId;
            SupplierId = supplierId;
            SupplierMaterialId = supplierMaterialId;
        }
    }

}
