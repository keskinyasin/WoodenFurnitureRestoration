using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract;

public interface IInvoiceService : IService<Invoice>
{
    Task<List<Invoice>> GetInvoicesByOrderAsync(int orderId);
    Task<List<Invoice>> GetInvoicesBySupplierAsync(int supplierId);
    Task<List<Invoice>> GetInvoicesByPaymentAsync(int paymentId);
    Task<List<Invoice>> GetInvoicesByShippingAsync(int shippingId);
    Task<List<Invoice>> GetInvoicesByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<Invoice?> GetInvoiceByOrderIdAsync(int orderId);
    Task<decimal> GetTotalRevenueAsync(DateTime? startDate = null, DateTime? endDate = null);
    Task<bool> ApplyDiscountAsync(int invoiceId, decimal discountAmount);
    Task<List<Invoice>> GetInvoicesByFiltersAsync(
        int? orderId = null,
        int? supplierId = null,
        int? paymentId = null,
        int? shippingId = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        decimal? minAmount = null,
        decimal? maxAmount = null);
}