using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Entities
{
    public class Restoration : IEntity
    {
        public Restoration() { } // Parametresiz yapıcı metot

        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen restorasyon adını belirtiniz.")]
        [Display(Name = "Restorasyon Adı")]
        [StringLength(100, ErrorMessage = "Restorasyon adı en fazla 100 karakter olmalıdır.")]
        public string RestorationName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen restorasyon açıklamasını belirtiniz.")]
        [Display(Name = "Restorasyon Açıklaması")]
        [StringLength(500, ErrorMessage = "Restorasyon açıklaması en fazla 500 karakter olmalıdır.")]
        public string RestorationDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen restorasyon fiyatını belirtiniz.")]
        [Display(Name = "Restorasyon Fiyatı")]
        public decimal RestorationPrice { get; set; }

        [Required(ErrorMessage = "Lütfen restorasyon görselini belirtiniz.")]
        [Display(Name = "Restorasyon Görseli")]
        public string RestorationImage { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen restorasyon durumunu belirtiniz.")]
        [Display(Name = "Restorasyon Durumu")]
        [StringLength(100, ErrorMessage = "Restorasyon durumu en fazla 100 karakter olmalıdır.")]
        public string RestorationStatus { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen restorasyon tarihini belirtiniz.")]
        [Display(Name = "Restorasyon Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RestorationDate { get; set; }

        [Required(ErrorMessage = "Lütfen restorasyon bitiş tarihini belirtiniz.")]
        [Display(Name = "Restorasyon Bitiş Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RestorationEndDate { get; set; }

        [Required(ErrorMessage = "Lütfen kategori belirtiniz.")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        // Category ile ilişki (N:1)
        public virtual Category Category { get; set; } = null!;

        // OrderDetail ile ilişki (1:N)
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        // Review ile ilişki (1:N)
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        // RestorationService ile ilişki (1:N)
        public virtual ICollection<RestorationService> RestorationServices { get; set; } = new List<RestorationService>();

        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

        // IEntity properties
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // Constructor with null checks
        public Restoration(string restorationName, string restorationDescription, decimal restorationPrice, string restorationImage, string restorationStatus, DateTime restorationDate, DateTime restorationEndDate, int categoryId)
        {
            RestorationName = restorationName ?? throw new ArgumentNullException(nameof(restorationName));
            RestorationDescription = restorationDescription ?? throw new ArgumentNullException(nameof(restorationDescription));
            RestorationPrice = restorationPrice;
            RestorationImage = restorationImage ?? throw new ArgumentNullException(nameof(restorationImage));
            RestorationStatus = restorationStatus ?? throw new ArgumentNullException(nameof(restorationStatus));
            RestorationDate = restorationDate;
            RestorationEndDate = restorationEndDate;
            CategoryId = categoryId;
        }
    }

}

