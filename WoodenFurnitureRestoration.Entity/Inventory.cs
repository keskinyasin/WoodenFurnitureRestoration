using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WoodenFurnitureRestoration.Entities
{
    public class Inventory : IEntity
    {
        public Inventory() { }

        // ✅ IEntity Properties (Basit Auto-Properties)
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // ✅ Inventory-Specific Properties
        [Required(ErrorMessage = "Lütfen stok miktarını belirtiniz.")]
        [Display(Name = "Stok Miktarı")]
        [Range(0, int.MaxValue, ErrorMessage = "Stok miktarı 0'dan küçük olamaz.")]
        public int QuantityInStock { get; set; }

        [Required(ErrorMessage = "Lütfen son güncelleme tarihini belirtiniz.")]
        [Display(Name = "Son Güncelleme Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdate { get; set; }

        [Required(ErrorMessage = "Lütfen fiyat belirtiniz.")]
        [Display(Name = "Fiyat")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Lütfen toplam miktarı belirtiniz.")]
        [Display(Name = "Toplam Miktar")]
        [Range(0, double.MaxValue, ErrorMessage = "Toplam miktar 0'dan küçük olamaz.")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Lütfen envanter tarihini belirtiniz.")]
        [Display(Name = "Envanter Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime InventoryDate { get; set; }

        // ✅ Foreign Keys
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int SupplierMaterialId { get; set; }

        [Required]
        public int AddressId { get; set; }

        // ✅ Navigation Properties
        public virtual Product Product { get; set; } = null!;
        public virtual SupplierMaterial SupplierMaterial { get; set; } = null!;
        public virtual Address Address { get; set; } = null!;

        // ✅ Collections (Circular Reference Önleme)
        [JsonIgnore]
        public virtual ICollection<ShippingInventory> ShippingInventories { get; set; } = new List<ShippingInventory>();

        // Constructor
        public Inventory(
            int quantityInStock,
            DateTime lastUpdate,
            decimal price,
            decimal totalAmount,
            DateTime inventoryDate,
            int productId,
            int supplierMaterialId,
            int addressId)
        {
            QuantityInStock = quantityInStock;
            LastUpdate = lastUpdate;
            Price = price;
            TotalAmount = totalAmount;
            InventoryDate = inventoryDate;
            ProductId = productId;
            SupplierMaterialId = supplierMaterialId;
            AddressId = addressId;
        }
    }
}