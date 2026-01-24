using System.ComponentModel.DataAnnotations;

namespace WoodenFurnitureRestoration.Shared.DTOs.Product
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Ürün adı zorunludur")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Fiyat zorunludur")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur")]
        [StringLength(500, MinimumLength = 10)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kategori seçimi zorunludur")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Tedarikçi seçimi zorunludur")]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Tedarikçi malzemesi seçimi zorunludur")]
        public int SupplierMaterialId { get; set; }
    }
}
