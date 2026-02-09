using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract;

public interface IOrderDetailService : IService<OrderDetail>
{
    // Custom Business Methods
    Task<List<OrderDetail>> GetOrderDetailsByOrderAsync(int orderId);
    Task<List<OrderDetail>> GetOrderDetailsByProductAsync(int productId);
    Task<List<OrderDetail>> GetOrderDetailsByRestorationAsync(int restorationId);
    Task<decimal> GetOrderTotalAsync(int orderId);
    Task<decimal> GetLineItemTotalAsync(int orderDetailId);
    Task<bool> UpdateQuantityAsync(int orderDetailId, int quantity);
    Task<List<OrderDetail>> GetOrderDetailsByFiltersAsync(
        int? orderId = null,
        int? productId = null,
        int? restorationId = null,
        int? minQuantity = null,
        int? maxQuantity = null,
        decimal? minUnitPrice = null,
        decimal? maxUnitPrice = null);
}