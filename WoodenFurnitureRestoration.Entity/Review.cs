using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WoodenFurnitureRestoration.Entities
{
    public class Review : IEntity
    {
        public Review() { }

        // ✅ IEntity Properties
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // ✅ Review-Specific Properties (SADECE GEREK OLANLAR)
        [Required(ErrorMessage = "Lütfen müşteri numarası belirtiniz.")]
        [Display(Name = "Müşteri Numarası")]
        [Range(1, int.MaxValue, ErrorMessage = "Müşteri numarası 1'den küçük olamaz.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Lütfen ürün numarası belirtiniz.")]
        [Display(Name = "Ürün Numarası")]
        [Range(1, int.MaxValue, ErrorMessage = "Ürün numarası 1'den küçük olamaz.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Lütfen tedarikçi numarası belirtiniz.")]
        [Display(Name = "Tedarikçi Numarası")]
        [Range(1, int.MaxValue, ErrorMessage = "Tedarikçi numarası 1'den küçük olamaz.")]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Lütfen tedarikçi malzeme numarası belirtiniz.")]
        [Display(Name = "Tedarikçi Malzeme Numarası")]
        [Range(1, int.MaxValue, ErrorMessage = "Tedarikçi malzeme numarası 1'den küçük olamaz.")]
        public int SupplierMaterialId { get; set; }

        [Required(ErrorMessage = "Lütfen restorasyon numarası belirtiniz.")]
        [Display(Name = "Restorasyon Numarası")]
        [Range(1, int.MaxValue, ErrorMessage = "Restorasyon numarası 1'den küçük olamaz.")]
        public int RestorationId { get; set; }

        [Required(ErrorMessage = "Yorum açıklaması boş bırakılamaz.")]
        [Display(Name = "Yorum Açıklaması")]
        [StringLength(500, ErrorMessage = "Yorum açıklaması 500 karakterden uzun olamaz.")]
        public string ReviewDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen yorum tarihi belirtiniz.")]
        [Display(Name = "Yorum Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReviewDate { get; set; }

        [Required(ErrorMessage = "Lütfen puan belirtiniz.")]
        [Display(Name = "Puan")]
        [Range(1, 5, ErrorMessage = "Puan 1 ile 5 arasında olmalıdır.")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Lütfen yorum durumunu belirtiniz.")]
        [Display(Name = "Yorum Durumu")]
        [StringLength(50, ErrorMessage = "Yorum durumu 50 karakterden uzun olamaz.")]
        public string ReviewStatus { get; set; } = string.Empty;

        // ✅ Navigation Properties (SADECE GEREK OLANLAR)
        [JsonIgnore]
        public virtual Customer Customer { get; set; } = null!;

        [JsonIgnore]
        public virtual Product Product { get; set; } = null!;

        [JsonIgnore]
        public virtual Supplier Supplier { get; set; } = null!;

        [JsonIgnore]
        public virtual SupplierMaterial SupplierMaterial { get; set; } = null!;

        [JsonIgnore]
        public virtual Restoration Restoration { get; set; } = null!;

        // Constructor
        public Review(
            int customerId,
            int productId,
            string reviewDescription,
            DateTime reviewDate,
            int rating,
            string reviewStatus)
        {
            CustomerId = customerId;
            ProductId = productId;
            ReviewDescription = reviewDescription ?? throw new ArgumentNullException(nameof(reviewDescription));
            ReviewDate = reviewDate;
            Rating = rating;
            ReviewStatus = reviewStatus ?? throw new ArgumentNullException(nameof(reviewStatus));
        }
    }
}