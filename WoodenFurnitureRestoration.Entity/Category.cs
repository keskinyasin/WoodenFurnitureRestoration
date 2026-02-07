using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WoodenFurnitureRestoration.Entities
{
    public class Category : IEntity
    {
        public Category() { }

        // ✅ IEntity Properties
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // ✅ Category-Specific Properties
        [Required(ErrorMessage = "Lütfen kategori adını belirtiniz.")]
        [Display(Name = "Kategori Adı")]
        [StringLength(100, ErrorMessage = "Kategori adı en fazla 100 karakter olmalıdır.")]
        public string CategoryName { get; set; } = string.Empty;

        [Display(Name = "Kategori Açıklaması")]
        [StringLength(500, ErrorMessage = "Kategori açıklaması en fazla 500 karakter olmalıdır.")]
        public string CategoryDescription { get; set; } = string.Empty;

        // ✅ Foreign Key - NULLABLE yapıldı!
        public int? SupplierId { get; set; }

        // ✅ Navigation Properties - NULLABLE yapıldı!
        [JsonIgnore]
        public virtual Supplier? Supplier { get; set; }

        // ✅ Collections
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        [JsonIgnore]
        public virtual ICollection<Restoration> Restorations { get; set; } = new List<Restoration>();

        [JsonIgnore]
        public virtual ICollection<RestorationService> RestorationServices { get; set; } = new List<RestorationService>();

        [JsonIgnore]
        public virtual ICollection<SupplierMaterial> SupplierMaterials { get; set; } = new List<SupplierMaterial>();

        [JsonIgnore]
        public virtual ICollection<SupplierCategory> SupplierCategories { get; set; } = new List<SupplierCategory>();

        [JsonIgnore]
        public virtual ICollection<CategoryTag> CategoryTags { get; set; } = new List<CategoryTag>();

        [JsonIgnore]
        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

        [JsonIgnore]
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

        // Constructor - SupplierId opsiyonel
        public Category(
            string categoryName,
            string categoryDescription,
            int? supplierId = null)
        {
            CategoryName = categoryName ?? throw new ArgumentNullException(nameof(categoryName));
            CategoryDescription = categoryDescription ?? string.Empty;
            SupplierId = supplierId;
        }
    }
}