using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class OrderService(IUnitOfWork unitOfWork, IMapper mapper)
    : Service<Order>(unitOfWork), IOrderService
{
    private readonly IMapper _mapper = mapper;
    protected override IRepository<Order> Repository => unitOfWork.OrderRepository;

    // Sipariş Durumları
    private static class OrderStatuses
    {
        public const string Pending = "Beklemede";
        public const string Processing = "İşleniyor";
        public const string Shipped = "Kargoya Verildi";
        public const string Delivered = "Teslim Edildi";
        public const string Cancelled = "İptal Edildi";
        public const string Completed = "Tamamlandı";
    }

    protected override void ValidateEntity(Order order)
    {
        if (order.CustomerId <= 0)
            throw new ArgumentException("Geçerli bir müşteri seçilmelidir.", nameof(order));
        if (order.SupplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi seçilmelidir.", nameof(order));
        if (order.SupplierMaterialId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi malzeme seçilmelidir.", nameof(order));
    }

    public async Task<List<Order>> GetOrdersByCustomerAsync(int customerId)
    {
        if (customerId <= 0)
            throw new ArgumentException("Geçerli bir müşteri ID'si gereklidir.", nameof(customerId));
        return await Repository.GetAllAsync(o => o.CustomerId == customerId && !o.Deleted);
    }

    public async Task<List<Order>> GetOrdersBySupplierAsync(int supplierId)
    {
        if (supplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi ID'si gereklidir.", nameof(supplierId));
        return await Repository.GetAllAsync(o => o.SupplierId == supplierId && !o.Deleted);
    }

    public async Task<List<Order>> GetOrdersByStatusAsync(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Sipariş durumu gereklidir.", nameof(status));
        return await Repository.GetAllAsync(o => o.OrderStatus == status && !o.Deleted);
    }

    public async Task<List<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
            throw new ArgumentException("Başlangıç tarihi bitiş tarihinden sonra olamaz.", nameof(startDate));
        return await Repository.GetAllAsync(o => o.OrderDate >= startDate && o.OrderDate <= endDate && !o.Deleted);
    }

    public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Sipariş durumu gereklidir.", nameof(status));
        var order = await Repository.FindAsync(orderId);
        if (order is null) return false;

        order.OrderStatus = status;
        order.UpdatedDate = DateTime.Now;
        await Repository.UpdateAsync(order);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AssignShippingAsync(int orderId, int shippingId)
    {
        if (shippingId <= 0)
            throw new ArgumentException("Geçerli bir kargo ID'si gereklidir.", nameof(shippingId));
        var order = await Repository.FindAsync(orderId);
        if (order is null) return false;

        order.ShippingId = shippingId;
        order.OrderStatus = OrderStatuses.Shipped;
        order.UpdatedDate = DateTime.Now;
        await Repository.UpdateAsync(order);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<List<Order>> GetPendingOrdersAsync()
    {
        return await Repository.GetAllAsync(o => o.OrderStatus == OrderStatuses.Pending && !o.Deleted);
    }

    public async Task<List<Order>> GetCompletedOrdersAsync()
    {
        return await Repository.GetAllAsync(o => o.OrderStatus == OrderStatuses.Completed && !o.Deleted);
    }

    public async Task<List<Order>> GetOrdersByFiltersAsync(
        int? customerId = null,
        int? supplierId = null,
        int? shippingId = null,
        string? status = null,
        DateTime? startDate = null,
        DateTime? endDate = null)
    {
        return await Repository.GetAllAsync(o =>
            !o.Deleted &&
            (!customerId.HasValue || o.CustomerId == customerId.Value) &&
            (!supplierId.HasValue || o.SupplierId == supplierId.Value) &&
            (!shippingId.HasValue || o.ShippingId == shippingId.Value) &&
            (string.IsNullOrEmpty(status) || o.OrderStatus == status) &&
            (!startDate.HasValue || o.OrderDate >= startDate.Value) &&
            (!endDate.HasValue || o.OrderDate <= endDate.Value));
    }
}