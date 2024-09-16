using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Entities
{
    public class RestorationService : IEntity
    {
        public RestorationService() { } // Parametresiz yapıcı metot

        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen restorasyon hizmeti adı belirtiniz.")]
        [Display(Name = "Restorasyon Hizmeti Adı")]
        [StringLength(50, ErrorMessage = "Restorasyon hizmeti adı 50 karakterden uzun olamaz.")]
        public string RestorationServiceName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen restorasyon hizmeti açıklaması belirtiniz.")]
        [Display(Name = "Restorasyon Hizmeti Açıklaması")]
        [StringLength(500, ErrorMessage = "Restorasyon hizmeti açıklaması 500 karakterden uzun olamaz.")]
        public string RestorationServiceDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen restorasyon hizmeti fiyatı belirtiniz.")]
        [Display(Name = "Restorasyon Hizmeti Fiyatı")]
        [Range(1, int.MaxValue, ErrorMessage = "Restorasyon hizmeti fiyatı 1'den küçük olamaz.")]
        public decimal RestorationServicePrice { get; set; }

        [Display(Name = "Restorasyon Hizmeti Resmi")]
        [StringLength(250, ErrorMessage = "Restorasyon hizmeti resmi 250 karakterden uzun olamaz.")]
        [DataType(DataType.ImageUrl)]
        public string RestorationServiceImage { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen restorasyon hizmeti durumu belirtiniz.")]
        [Display(Name = "Restorasyon Hizmeti Durumu")]
        [StringLength(50, ErrorMessage = "Restorasyon hizmeti durumu 50 karakterden uzun olamaz.")]
        public string RestorationServiceStatus { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen restorasyon hizmeti tarihi belirtiniz.")]
        [Display(Name = "Restorasyon Hizmeti Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RestorationServiceDate { get; set; }

        [Required(ErrorMessage = "Lütfen kategori belirtiniz.")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Lütfen restorasyon belirtiniz.")]
        [Display(Name = "Restorasyon")]
        public int RestorationId { get; set; }

        // Navigation properties
        public virtual Category Category { get; set; } = null!;
        public virtual Restoration Restoration { get; set; } = null!;

        // IEntity properties
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // Constructor with null checks
        public RestorationService(string restorationServiceName, string restorationServiceDescription, decimal restorationServicePrice, string restorationServiceImage, string restorationServiceStatus, DateTime restorationServiceDate, int categoryId, int restorationId)
        {
            RestorationServiceName = restorationServiceName ?? throw new ArgumentNullException(nameof(restorationServiceName));
            RestorationServiceDescription = restorationServiceDescription ?? throw new ArgumentNullException(nameof(restorationServiceDescription));
            RestorationServicePrice = restorationServicePrice;
            RestorationServiceImage = restorationServiceImage ?? throw new ArgumentNullException(nameof(restorationServiceImage));
            RestorationServiceStatus = restorationServiceStatus ?? throw new ArgumentNullException(nameof(restorationServiceStatus));
            RestorationServiceDate = restorationServiceDate;
            CategoryId = categoryId;
            RestorationId = restorationId;
        }
    }

}

