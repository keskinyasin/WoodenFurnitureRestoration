using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract;

public interface IShippingService : IService<Shipping>
{
    Task<List<Shipping>> GetShippingsByOrderAsync(int orderId);
    Task<List<Shipping>> GetShippingsByAddressAsync(int addressId);
    Task<List<Shipping>> GetShippingsBySupplierAsync(int supplierId);
    Task<List<Shipping>> GetShippingsByStatusAsync(ShippingStatus status);
    Task<List<Shipping>> GetShippingsByTypeAsync(ShippingType type);
    Task<List<Shipping>> GetShippingsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<bool> UpdateStatusAsync(int shippingId, ShippingStatus status);
    Task<bool> MarkAsShippedAsync(int shippingId);
    Task<bool> MarkAsDeliveredAsync(int shippingId);
    Task<bool> CancelShippingAsync(int shippingId);
    Task<List<Shipping>> GetPendingShippingsAsync();
    Task<List<Shipping>> GetDeliveredShippingsAsync();
    Task<decimal> GetTotalShippingCostByOrderAsync(int orderId);
    Task<List<Shipping>> GetShippingsByFiltersAsync(
        int? orderId = null,
        int? addressId = null,
        int? supplierId = null,
        ShippingStatus? status = null,
        ShippingType? type = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        decimal? minCost = null,
        decimal? maxCost = null);
}