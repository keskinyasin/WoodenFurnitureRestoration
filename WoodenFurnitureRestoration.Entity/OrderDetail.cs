using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Entities
{
    public class OrderDetail : IEntity
    {
        public OrderDetail() { } // Parametresiz yapıcı metot

        public int Id { get; set; } // IEntity'den miras alınan Id özelliği

        [Required(ErrorMessage = "Lütfen sipariş numarası belirtiniz.")]
        [Display(Name = "Sipariş Numarası")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Lütfen restorasyon numarası belirtiniz.")]
        [Display(Name = "Restorasyon Numarası")]
        public int RestorationId { get; set; }

        [Required(ErrorMessage = "Lütfen ürün numarası belirtiniz.")]
        [Display(Name = "Ürün Numarası")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Lütfen miktar belirtiniz.")]
        [Display(Name = "Miktar")]
        [Range(1, int.MaxValue, ErrorMessage = "Miktar 1'den küçük olamaz.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Miktar sadece rakam içerebilir.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Lütfen birim fiyat belirtiniz.")]
        [Display(Name = "Birim Fiyat")]
        [Range(1, double.MaxValue, ErrorMessage = "Birim fiyat 1'den küçük olamaz.")]
        public decimal UnitPrice { get; set; }

        // Navigation properties
        public virtual Order Order { get; set; } = null!;
        public virtual Restoration Restoration { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;

        // IEntity properties
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // Constructor with null checks
        public OrderDetail(int orderId, int restorationId, int productId, int quantity, decimal unitPrice)
        {
            OrderId = orderId;
            RestorationId = restorationId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }

}

