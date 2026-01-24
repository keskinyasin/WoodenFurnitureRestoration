using System.ComponentModel.DataAnnotations;

namespace WoodenFurnitureRestoration.Shared.DTOs.Order
{
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "Müşteri seçimi zorunludur")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Tedarikçi seçimi zorunludur")]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Tedarikçi malzemesi seçimi zorunludur")]
        public int SupplierMaterialId { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Pending";

        public int? ShippingId { get; set; }
    }
}
