using System.ComponentModel.DataAnnotations;

namespace WoodenFurnitureRestoration.Entities
{
    public class Product : IEntity
    {
        public Product() { } // Parametresiz yapıcı metot

        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen ürün adını belirtiniz.")]
        [Display(Name = "Ürün Adı")]
        [StringLength(100, ErrorMessage = "Ürün adı en fazla 100 karakter olmalıdır.")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen ürün fiyatını belirtiniz.")]
        [Display(Name = "Ürün Fiyatı")]
        [Range(1, double.MaxValue, ErrorMessage = "Ürün fiyatı 1'den küçük olamaz.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Lütfen ürün açıklamasını belirtiniz.")]
        [Display(Name = "Ürün Açıklaması")]
        [StringLength(500, ErrorMessage = "Ürün açıklaması en fazla 500 karakter olmalıdır.")]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        [Required]
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; } = null!;

        [Required]
        public int SupplierMaterialId { get; set; }
        public virtual SupplierMaterial SupplierMaterial { get; set; } = null!;

        public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<ShippingTag> ShippingTags { get; set; } = new List<ShippingTag>();
        public virtual ICollection<ShippingProduct> ShippingProducts { get; set; } = new List<ShippingProduct>();
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public virtual ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();

        // IEntity properties
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // Constructor with null checks
        public Product(string productName, decimal price, string description, int categoryId, int supplierId, int supplierMaterialId)
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

