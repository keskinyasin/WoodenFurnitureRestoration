namespace WoodenFurnitureRestoration.Shared.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public int SupplierId { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public int SupplierMaterialId { get; set; }
        public decimal TotalAmount { get; set; }
        public int? ShippingId { get; set; }
    }
}
