using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Entities
{
    public class Tag : IEntity
    {
        public Tag() { }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<BlogPostTag> BlogPostTags { get; set; } = new List<BlogPostTag>();
        public ICollection<PaymentTag> PaymentTags { get; set; } = new List<PaymentTag>();

        // Product ile ilişki
        public ICollection<Product> Products { get; set; } = new List<Product>();

        // Category ile ilişki
        public ICollection<Category> Categories { get; set; } = new List<Category>();

        // SupplierMaterial ile ilişki
        public ICollection<SupplierMaterialTag> SupplierMaterialTags { get; set; } = new List<SupplierMaterialTag>();

        // Invoice ile ilişki
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

        // Order ile ilişki
        public ICollection<Order> Orders { get; set; } = new List<Order>();

        // Payment ile ilişki
        public ICollection<ShippingTag> ShippingTags { get; set; } = new List<ShippingTag>();

        public ICollection<CategoryTag> CategoryTags { get; set; } = new List<CategoryTag>();
        public ICollection<InvoiceTag> InvoiceTags { get; set; } = new List<InvoiceTag>();
        public ICollection<OrderTag> OrderTags { get; set; } = new List<OrderTag>();
        public ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();

        public virtual ICollection<Shipping> Shippings { get; set; } = new List<Shipping>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

        // IEntity'den gelen özellikler:
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // Constructor with null checks
        public Tag(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}

