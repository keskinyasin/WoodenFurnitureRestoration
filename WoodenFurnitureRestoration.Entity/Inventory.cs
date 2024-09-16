using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Entities
{
    public class Inventory : IEntity
    {
        public Inventory() { } // Parametresiz yapıcı metot

        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen stok miktarını belirtiniz.")]
        [Display(Name = "Stok Miktarı")]
        [Range(1, int.MaxValue, ErrorMessage = "Stok miktarı 1'den küçük olamaz.")]
        public int QuantityInStock { get; set; }

        [Required(ErrorMessage = "Lütfen son güncelleme tarihini belirtiniz.")]
        [Display(Name = "Son Güncelleme Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdate { get; set; }

        [Required(ErrorMessage = "Lütfen fiyat belirtiniz.")]
        [Display(Name = "Fiyat")]
        [Range(1, double.MaxValue, ErrorMessage = "Fiyat 1'den küçük olamaz.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Lütfen toplam miktarı belirtiniz.")]
        [Display(Name = "Toplam Miktar")]
        [Range(1, double.MaxValue, ErrorMessage = "Toplam miktar 1'den küçük olamaz.")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Lütfen envanter tarihini belirtiniz.")]
        [Display(Name = "Envanter Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime InventoryDate { get; set; }

        // Relationships
        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;

        [Required]
        public int SupplierMaterialId { get; set; }
        public virtual SupplierMaterial SupplierMaterial { get; set; } = null!;

        [Required]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; } = null!;

        public virtual ICollection<ShippingInventory> ShippingInventories { get; set; } = new List<ShippingInventory>();

        // IEntity properties
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // Constructor with null checks
        public Inventory(int quantityInStock, DateTime lastUpdate, decimal price, decimal totalAmount, DateTime inventoryDate, int productId, int supplierMaterialId, int addressId)
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

