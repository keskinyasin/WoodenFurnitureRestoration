using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WoodenFurnitureRestoration.Entities
{
    public class OrderDetail : IEntity
    {
        public OrderDetail() { }

        // ✅ IEntity Properties (Basit Auto-Properties)
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // ✅ OrderDetail-Specific Properties
        [Required(ErrorMessage = "Lütfen sipariş numarası belirtiniz.")]
        [Display(Name = "Sipariş Numarası")]
        [Range(1, int.MaxValue, ErrorMessage = "Sipariş numarası 1'den küçük olamaz.")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Lütfen restorasyon numarası belirtiniz.")]
        [Display(Name = "Restorasyon Numarası")]
        [Range(1, int.MaxValue, ErrorMessage = "Restorasyon numarası 1'den küçük olamaz.")]
        public int RestorationId { get; set; }

        [Required(ErrorMessage = "Lütfen ürün numarası belirtiniz.")]
        [Display(Name = "Ürün Numarası")]
        [Range(1, int.MaxValue, ErrorMessage = "Ürün numarası 1'den küçük olamaz.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Lütfen miktar belirtiniz.")]
        [Display(Name = "Miktar")]
        [Range(1, int.MaxValue, ErrorMessage = "Miktar 1'den küçük olamaz.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Lütfen birim fiyat belirtiniz.")]
        [Display(Name = "Birim Fiyat")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Birim fiyat 0'dan büyük olmalıdır.")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        // ✅ Navigation Properties
        [JsonIgnore]
        public virtual Order Order { get; set; } = null!;

        [JsonIgnore]
        public virtual Restoration Restoration { get; set; } = null!;

        [JsonIgnore]
        public virtual Product Product { get; set; } = null!;

        // Constructor
        public OrderDetail(
            int orderId,
            int restorationId,
            int productId,
            int quantity,
            decimal unitPrice)
        {
            OrderId = orderId;
            RestorationId = restorationId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}