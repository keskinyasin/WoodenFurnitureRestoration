using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Entities
{
    public class SupplierMaterial : IEntity
    {
        public SupplierMaterial() { }

        public int Id { get; set; }

        public int SupplierId { get; set; }

        [Display(Name = "Malzeme Adedi")]
        [Range(1, int.MaxValue, ErrorMessage = "Malzeme adedi 1'den küçük olamaz.")]
        public int MaterialQuantity { get; set; }

        [Display(Name = "Malzeme Durumu")]
        [Required(ErrorMessage = "Malzeme durumu boş bırakılamaz.")]
        [StringLength(50, ErrorMessage = "Malzeme durumu 50 karakterden uzun olamaz.")]
        public string MaterialStatus { get; set; } = string.Empty;

        [Display(Name = "Malzeme Adı")]
        [Required(ErrorMessage = "Malzeme adı boş bırakılamaz.")]
        [StringLength(150, ErrorMessage = "Malzeme adı 150 karakterden uzun olamaz.")]
        public string MaterialName { get; set; } = string.Empty;

        [Display(Name = "Malzeme Fiyatı")]
        [Required(ErrorMessage = "Malzeme fiyatı boş bırakılamaz.")]
        [Range(1, int.MaxValue, ErrorMessage = "Malzeme fiyatı 1'den küçük olamaz.")]
        public decimal MaterialPrice { get; set; }

        [Display(Name = "Malzeme Açıklaması")]
        [Required(ErrorMessage = "Malzeme açıklaması boş bırakılamaz.")]
        [StringLength(500, ErrorMessage = "Malzeme açıklaması 500 karakterden uzun olamaz.")]
        public string MaterialDescription { get; set; } = string.Empty;

        [Display(Name = "Malzeme Resmi")]
        public string MaterialImage { get; set; } = string.Empty;

        [Display(Name = "Malzeme Kategorisi")]
        [Required(ErrorMessage = "Malzeme kategorisi boş bırakılamaz.")]
        [StringLength(50, ErrorMessage = "Malzeme kategorisi 50 karakterden uzun olamaz.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Malzeme kategorisi sadece harf içerebilir.")]
        public string MaterialCategory { get; set; } = string.Empty;

        [Display(Name = "Malzeme Türü")]
        [Required(ErrorMessage = "Malzeme türü boş bırakılamaz.")]
        [StringLength(50, ErrorMessage = "Malzeme türü 50 karakterden uzun olamaz.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Malzeme türü sadece harf içerebilir.")]
        public string MaterialType { get; set; } = string.Empty;

        [Display(Name = "Malzeme Rengi")]
        public string MaterialColor { get; set; } = string.Empty;

        [Display(Name = "Malzeme Boyutu")]
        public string MaterialSize { get; set; } = string.Empty;

        [Display(Name = "Malzeme Markası")]
        public string MaterialBrand { get; set; } = string.Empty;

        [Display(Name = "Malzeme Modeli")]
        public string MaterialModel { get; set; } = string.Empty;

        public virtual Supplier Supplier { get; set; } = null!;

        public int? AddressId { get; set; }
        public virtual Address Address { get; set; } = null!;

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
        public virtual ICollection<Shipping> Shippings { get; set; } = new List<Shipping>();
        public virtual ICollection<SupplierMaterialTag> SupplierMaterialTags { get; set; } = new List<SupplierMaterialTag>();

        // IEntity ile uyumlu olmak için gerekli ek özellikler
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // Constructor with null checks
        public SupplierMaterial(int supplierId, int materialQuantity, string materialStatus, string materialName, decimal materialPrice, string materialDescription, string materialImage, string materialCategory, string materialType, string materialColor, string materialSize, string materialBrand, string materialModel, int categoryId)
        {
            SupplierId = supplierId;
            MaterialQuantity = materialQuantity;
            MaterialStatus = materialStatus ?? throw new ArgumentNullException(nameof(materialStatus));
            MaterialName = materialName ?? throw new ArgumentNullException(nameof(materialName));
            MaterialPrice = materialPrice;
            MaterialDescription = materialDescription ?? throw new ArgumentNullException(nameof(materialDescription));
            MaterialImage = materialImage ?? throw new ArgumentNullException(nameof(materialImage));
            MaterialCategory = materialCategory ?? throw new ArgumentNullException(nameof(materialCategory));
            MaterialType = materialType ?? throw new ArgumentNullException(nameof(materialType));
            MaterialColor = materialColor ?? throw new ArgumentNullException(nameof(materialColor));
            MaterialSize = materialSize ?? throw new ArgumentNullException(nameof(materialSize));
            MaterialBrand = materialBrand ?? throw new ArgumentNullException(nameof(materialBrand));
            MaterialModel = materialModel ?? throw new ArgumentNullException(nameof(materialModel));
            CategoryId = categoryId;
        }
    }
}


