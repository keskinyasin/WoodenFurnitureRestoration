using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract;

public interface IPaymentService : IService<Payment>
{
    Task<List<Payment>> GetPaymentsByOrderAsync(int orderId);
    Task<List<Payment>> GetPaymentsBySupplierAsync(int supplierId);
    Task<List<Payment>> GetPaymentsByAddressAsync(int addressId);
    Task<List<Payment>> GetPaymentsByStatusAsync(string status);
    Task<List<Payment>> GetPaymentsByMethodAsync(string method);
    Task<List<Payment>> GetPaymentsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<bool> UpdatePaymentStatusAsync(int paymentId, string status);
    Task<decimal> GetTotalPaymentsByOrderAsync(int orderId);
    Task<decimal> GetTotalRevenueAsync(DateTime? startDate = null, DateTime? endDate = null);
    Task<List<Payment>> GetPendingPaymentsAsync();
    Task<List<Payment>> GetCompletedPaymentsAsync();
    Task<List<Payment>> GetPaymentsByFiltersAsync(
        int? orderId = null,
        int? supplierId = null,
        int? addressId = null,
        int? shippingId = null,
        string? status = null,
        string? method = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        decimal? minAmount = null,
        decimal? maxAmount = null);
}