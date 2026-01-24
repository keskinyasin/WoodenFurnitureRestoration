using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WoodenFurnitureRestoration.Entities
{
    public class RestorationService : IEntity
    {
        public RestorationService() { }

        // ✅ IEntity Properties (Basit Auto-Properties)
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // ✅ RestorationService-Specific Properties
        [Required(ErrorMessage = "Lütfen restorasyon hizmeti adı belirtiniz.")]
        [Display(Name = "Restorasyon Hizmeti Adı")]
        [StringLength(50, ErrorMessage = "Restorasyon hizmeti adı 50 karakterden uzun olamaz.")]
        public string RestorationServiceName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen restorasyon hizmeti açıklamasını belirtiniz.")]
        [Display(Name = "Restorasyon Hizmeti Açıklaması")]
        [StringLength(500, ErrorMessage = "Restorasyon hizmeti açıklaması 500 karakterden uzun olamaz.")]
        public string RestorationServiceDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen restorasyon hizmeti fiyatını belirtiniz.")]
        [Display(Name = "Restorasyon Hizmeti Fiyatı")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Restorasyon hizmeti fiyatı 0'dan büyük olmalıdır.")]
        [DataType(DataType.Currency)]
        public decimal RestorationServicePrice { get; set; }

        [Display(Name = "Restorasyon Hizmeti Resmi")]
        [StringLength(250, ErrorMessage = "Restorasyon hizmeti resmi 250 karakterden uzun olamaz.")]
        [DataType(DataType.ImageUrl, ErrorMessage = "Geçersiz görsel URL'si.")]
        public string? RestorationServiceImage { get; set; }

        [Required(ErrorMessage = "Lütfen restorasyon hizmeti durumunu belirtiniz.")]
        [Display(Name = "Restorasyon Hizmeti Durumu")]
        [StringLength(50, ErrorMessage = "Restorasyon hizmeti durumu 50 karakterden uzun olamaz.")]
        public string RestorationServiceStatus { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen restorasyon hizmeti tarihini belirtiniz.")]
        [Display(Name = "Restorasyon Hizmeti Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RestorationServiceDate { get; set; }

        // ✅ Foreign Keys
        [Required(ErrorMessage = "Lütfen kategori belirtiniz.")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Lütfen restorasyon belirtiniz.")]
        [Display(Name = "Restorasyon")]
        public int RestorationId { get; set; }

        // ✅ Navigation Properties
        [JsonIgnore]
        public virtual Category Category { get; set; } = null!;

        [JsonIgnore]
        public virtual Restoration Restoration { get; set; } = null!;

        // Constructor
        public RestorationService(
            string restorationServiceName,
            string restorationServiceDescription,
            decimal restorationServicePrice,
            string restorationServiceStatus,
            DateTime restorationServiceDate,
            int categoryId,
            int restorationId,
            string? restorationServiceImage = null)
        {
            RestorationServiceName = restorationServiceName ?? throw new ArgumentNullException(nameof(restorationServiceName));
            RestorationServiceDescription = restorationServiceDescription ?? throw new ArgumentNullException(nameof(restorationServiceDescription));
            RestorationServicePrice = restorationServicePrice;
            RestorationServiceStatus = restorationServiceStatus ?? throw new ArgumentNullException(nameof(restorationServiceStatus));
            RestorationServiceDate = restorationServiceDate;
            CategoryId = categoryId;
            RestorationId = restorationId;
            RestorationServiceImage = restorationServiceImage;
        }
    }
}