using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WoodenFurnitureRestoration.Entities
{
    public class Tag : IEntity
    {
        public Tag() { }

        // ✅ IEntity Properties (Basit Auto-Properties)
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // ✅ Tag-Specific Properties
        [Required(ErrorMessage = "Lütfen etiket adını belirtiniz.")]
        [Display(Name = "Etiket Adı")]
        [StringLength(100, ErrorMessage = "Etiket adı 100 karakterden uzun olamaz.")]
        public string Name { get; set; } = string.Empty;

        // ✅ Join Table Collections SADECE (Circular Reference Önleme)
        [JsonIgnore]
        public virtual ICollection<BlogPostTag> BlogPostTags { get; set; } = new List<BlogPostTag>();

        [JsonIgnore]
        public virtual ICollection<CategoryTag> CategoryTags { get; set; } = new List<CategoryTag>();

        [JsonIgnore]
        public virtual ICollection<InvoiceTag> InvoiceTags { get; set; } = new List<InvoiceTag>();

        [JsonIgnore]
        public virtual ICollection<OrderTag> OrderTags { get; set; } = new List<OrderTag>();

        [JsonIgnore]
        public virtual ICollection<PaymentTag> PaymentTags { get; set; } = new List<PaymentTag>();

        [JsonIgnore]
        public virtual ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();

        [JsonIgnore]
        public virtual ICollection<ShippingTag> ShippingTags { get; set; } = new List<ShippingTag>();

        [JsonIgnore]
        public virtual ICollection<SupplierMaterialTag> SupplierMaterialTags { get; set; } = new List<SupplierMaterialTag>();

        // Constructor
        public Tag(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}