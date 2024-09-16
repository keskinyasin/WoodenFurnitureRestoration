using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Entities
{
    public class Review : IEntity
    {
        public Review() { } // Parametresiz yapıcı metot

        public int Id { get; set; } // IEntity'den miras alınan Id özelliği

        [Required(ErrorMessage = "Lütfen müşteri numarası belirtiniz.")]
        [Display(Name = "Müşteri Numarası")]
        [Range(1, int.MaxValue, ErrorMessage = "Müşteri numarası 1'den küçük olamaz.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Müşteri numarası sadece rakam içerebilir.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Lütfen ürün numarası belirtiniz.")]
        [Display(Name = "Ürün Numarası")]
        [Range(1, int.MaxValue, ErrorMessage = "Ürün numarası 1'den küçük olamaz.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Ürün numarası sadece rakam içerebilir.")]
        public int ProductId { get; set; }

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
        [RegularExpression("^[0-9]*$", ErrorMessage = "Puan sadece rakam içerebilir.")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Lütfen yorum durumu belirtiniz.")]
        [Display(Name = "Yorum Durumu")]
        [StringLength(50, ErrorMessage = "Yorum durumu 50 karakterden uzun olamaz.")]
        public string ReviewStatus { get; set; } = string.Empty; // Yorum durumu için eklenmiş alan

        // Navigation properties
        public virtual Customer Customer { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;

        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; } = null!;

        public int SupplierMaterialId { get; set; }
        public virtual SupplierMaterial SupplierMaterial { get; set; } = null!;

        public int RestorationId { get; set; }
        public virtual Restoration Restoration { get; set; } = null!;

        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

        // IEntity properties
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // Constructor with null checks
        public Review(int customerId, int productId, string reviewDescription, DateTime reviewDate, int rating, string reviewStatus, int supplierId, int supplierMaterialId, int restorationId)
        {
            CustomerId = customerId;
            ProductId = productId;
            ReviewDescription = reviewDescription ?? throw new ArgumentNullException(nameof(reviewDescription));
            ReviewDate = reviewDate;
            Rating = rating;
            ReviewStatus = reviewStatus ?? throw new ArgumentNullException(nameof(reviewStatus));
            SupplierId = supplierId;
            SupplierMaterialId = supplierMaterialId;
            RestorationId = restorationId;
        }
    }

}
