using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WoodenFurnitureRestoration.Entities
{
    public class SupplierMaterial : IEntity
    {
        public SupplierMaterial() { }

        // ✅ IEntity Properties (Basit Auto-Properties)
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // ✅ SupplierMaterial-Specific Properties
        [Display(Name = "Malzeme Adedi")]
        [Range(0, int.MaxValue, ErrorMessage = "Malzeme adedi 0'dan küçük olamaz.")]
        public int MaterialQuantity { get; set; }

        [Required(ErrorMessage = "Lütfen malzeme durumunu belirtiniz.")]
        [Display(Name = "Malzeme Durumu")]
        [StringLength(50, ErrorMessage = "Malzeme durumu 50 karakterden uzun olamaz.")]
        public string MaterialStatus { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen malzeme adını belirtiniz.")]
        [Display(Name = "Malzeme Adı")]
        [StringLength(150, ErrorMessage = "Malzeme adı 150 karakterden uzun olamaz.")]
        public string MaterialName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen malzeme fiyatını belirtiniz.")]
        [Display(Name = "Malzeme Fiyatı")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Malzeme fiyatı 0'dan büyük olmalıdır.")]
        [DataType(DataType.Currency)]
        public decimal MaterialPrice { get; set; }

        [Required(ErrorMessage = "Lütfen malzeme açıklamasını belirtiniz.")]
        [Display(Name = "Malzeme Açıklaması")]
        [StringLength(500, ErrorMessage = "Malzeme açıklaması 500 karakterden uzun olamaz.")]
        public string MaterialDescription { get; set; } = string.Empty;

        [Display(Name = "Malzeme Resmi")]
        [DataType(DataType.ImageUrl, ErrorMessage = "Geçersiz görsel URL'si.")]
        public string? MaterialImage { get; set; }

        [Required(ErrorMessage = "Lütfen malzeme kategorisini belirtiniz.")]
        [Display(Name = "Malzeme Kategorisi")]
        [StringLength(50, ErrorMessage = "Malzeme kategorisi 50 karakterden uzun olamaz.")]
        public string MaterialCategory { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen malzeme türünü belirtiniz.")]
        [Display(Name = "Malzeme Türü")]
        [StringLength(50, ErrorMessage = "Malzeme türü 50 karakterden uzun olamaz.")]
        public string MaterialType { get; set; } = string.Empty;

        [Display(Name = "Malzeme Rengi")]
        [StringLength(50, ErrorMessage = "Malzeme rengi 50 karakterden uzun olamaz.")]
        public string? MaterialColor { get; set; }

        [Display(Name = "Malzeme Boyutu")]
        [StringLength(50, ErrorMessage = "Malzeme boyutu 50 karakterden uzun olamaz.")]
        public string? MaterialSize { get; set; }

        [Display(Name = "Malzeme Markası")]
        [StringLength(50, ErrorMessage = "Malzeme markası 50 karakterden uzun olamaz.")]
        public string? MaterialBrand { get; set; }

        [Display(Name = "Malzeme Modeli")]
        [StringLength(50, ErrorMessage = "Malzeme modeli 50 karakterden uzun olamaz.")]
        public string? MaterialModel { get; set; }

        // ✅ Foreign Keys
        [Required]
        public int SupplierId { get; set; }

        public int? AddressId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        // ✅ Navigation Properties
        [JsonIgnore]
        public virtual Supplier Supplier { get; set; } = null!;

        [JsonIgnore]
        public virtual Address? Address { get; set; }

        [JsonIgnore]
        public virtual Category Category { get; set; } = null!;

        // ✅ Collections (Circular Reference Önleme)
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        [JsonIgnore]
        public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

        [JsonIgnore]
        public virtual ICollection<SupplierMaterialTag> SupplierMaterialTags { get; set; } = new List<SupplierMaterialTag>();

        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        [JsonIgnore]
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

        [JsonIgnore]
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

        [JsonIgnore]
        public virtual ICollection<Shipping> Shippings { get; set; } = new List<Shipping>();

        [JsonIgnore]
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        // Constructor
        public SupplierMaterial(
            int supplierId,
            int categoryId,
            string materialStatus,
            string materialName,
            decimal materialPrice,
            string materialDescription,
            string materialCategory,
            string materialType,
            int materialQuantity = 0,
            string? materialImage = null,
            string? materialColor = null,
            string? materialSize = null,
            string? materialBrand = null,
            string? materialModel = null,
            int? addressId = null)
        {
            SupplierId = supplierId;
            CategoryId = categoryId;
            MaterialStatus = materialStatus ?? throw new ArgumentNullException(nameof(materialStatus));
            MaterialName = materialName ?? throw new ArgumentNullException(nameof(materialName));
            MaterialPrice = materialPrice;
            MaterialDescription = materialDescription ?? throw new ArgumentNullException(nameof(materialDescription));
            MaterialCategory = materialCategory ?? throw new ArgumentNullException(nameof(materialCategory));
            MaterialType = materialType ?? throw new ArgumentNullException(nameof(materialType));
            MaterialQuantity = materialQuantity;
            MaterialImage = materialImage;
            MaterialColor = materialColor;
            MaterialSize = materialSize;
            MaterialBrand = materialBrand;
            MaterialModel = materialModel;
            AddressId = addressId;
        }
    }
}