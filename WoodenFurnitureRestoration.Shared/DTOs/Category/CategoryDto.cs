namespace WoodenFurnitureRestoration.Shared.DTOs.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int SupplierId { get; set; } 
        public string SupplierName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
