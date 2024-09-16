using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Entities
{
    public class Category : IEntity
    {
        public Category() { } // Parametresiz yapıcı metot

        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen kategori adını belirtiniz.")]
        [Display(Name = "Kategori Adı")]
        [StringLength(100, ErrorMessage = "Kategori adı en fazla 100 karakter olmalıdır.")]
        public string CategoryName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen kategori açıklaması belirtiniz.")]
        [Display(Name = "Kategori Açıklaması")]
        [StringLength(500, ErrorMessage = "Kategori açıklaması en fazla 500 karakter olmalıdır.")]
        public string CategoryDescription { get; set; } = string.Empty;

        [Required]
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<Restoration> Restorations { get; set; } = new List<Restoration>();
        public virtual ICollection<RestorationService> RestorationServices { get; set; } = new List<RestorationService>();
        public virtual ICollection<SupplierMaterial> SupplierMaterials { get; set; } = new List<SupplierMaterial>();
        public virtual ICollection<SupplierCategory> SupplierCategories { get; set; } = new List<SupplierCategory>();
        public virtual ICollection<CategoryTag> CategoryTags { get; set; } = new List<CategoryTag>();
        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

        // IEntity properties
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // Constructor with null checks
        public Category(string categoryName, string categoryDescription, int supplierId)
        {
            CategoryName = categoryName ?? throw new ArgumentNullException(nameof(categoryName));
            CategoryDescription = categoryDescription ?? throw new ArgumentNullException(nameof(categoryDescription));
            SupplierId = supplierId;
        }
    }

}

