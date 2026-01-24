using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WoodenFurnitureRestoration.Entities
{
    public class Product : IEntity
    {
        public Product() { }

        // ✅ IEntity Properties (Basit Auto-Properties)
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // ✅ Product-Specific Properties
        [Required(ErrorMessage = "Lütfen ürün adını belirtiniz.")]
        [Display(Name = "Ürün Adı")]
        [StringLength(100, ErrorMessage = "Ürün adı en fazla 100 karakter olmalıdır.")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen ürün fiyatını belirtiniz.")]
        [Display(Name = "Ürün Fiyatı")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Ürün fiyatı 0'dan büyük olmalıdır.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Lütfen ürün açıklamasını belirtiniz.")]
        [Display(Name = "Ürün Açıklaması")]
        [StringLength(500, ErrorMessage = "Ürün açıklaması en fazla 500 karakter olmalıdır.")]
        public string Description { get; set; } = string.Empty;

        // ✅ Foreign Keys
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Required]
        public int SupplierMaterialId { get; set; }

        // ✅ Navigation Properties
        [JsonIgnore]
        public virtual Category Category { get; set; } = null!;

        [JsonIgnore]
        public virtual Supplier Supplier { get; set; } = null!;

        [JsonIgnore]
        public virtual SupplierMaterial SupplierMaterial { get; set; } = null!;

        // ✅ Collections (Circular Reference Önleme)
        [JsonIgnore]
        public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        [JsonIgnore]
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        [JsonIgnore]
        public virtual ICollection<ShippingProduct> ShippingProducts { get; set; } = new List<ShippingProduct>();

        [JsonIgnore]
        public virtual ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();

        [JsonIgnore]
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

        // Constructor
        public Product(
            string productName,
            decimal price,
            string description,
            int categoryId,
            int supplierId,
            int supplierMaterialId)
        {
            ProductName = productName ?? throw new ArgumentNullException(nameof(productName));
            Price = price;
            Description = description ?? throw new ArgumentNullException(nameof(description));
            CategoryId = categoryId;
            SupplierId = supplierId;
            SupplierMaterialId = supplierMaterialId;
        }
    }
}