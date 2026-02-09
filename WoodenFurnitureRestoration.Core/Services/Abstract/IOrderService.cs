using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract;

public interface IOrderService : IService<Order>
{
    // Custom Business Methods
    Task<List<Order>> GetOrdersByCustomerAsync(int customerId);
    Task<List<Order>> GetOrdersBySupplierAsync(int supplierId);
    Task<List<Order>> GetOrdersByStatusAsync(string status);
    Task<List<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<bool> UpdateOrderStatusAsync(int orderId, string status);
    Task<bool> AssignShippingAsync(int orderId, int shippingId);
    Task<List<Order>> GetPendingOrdersAsync();
    Task<List<Order>> GetCompletedOrdersAsync();
    Task<List<Order>> GetOrdersByFiltersAsync(
        int? customerId = null,
        int? supplierId = null,
        int? shippingId = null,
        string? status = null,
        DateTime? startDate = null,
        DateTime? endDate = null);
}