using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class OrderService(IUnitOfWork unitOfWork, IMapper mapper) : IOrderService
{
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

    #region CRUD Operations

    public async Task<List<Order>> GetAllAsync()
    {
        return await unitOfWork.OrderRepository.GetAllAsync(o => !o.Deleted);
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Geçerli bir sipariş ID'si gereklidir.", nameof(id));

        return await unitOfWork.OrderRepository.FindAsync(id);
    }

    public async Task<int> CreateAsync(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);
        ValidateOrder(order);

        try
        {
            order.CreatedDate = DateTime.Now;
            order.UpdatedDate = DateTime.Now;
            order.OrderDate = DateTime.Now;
            order.Deleted = false;
            order.OrderStatus = OrderStatuses.Pending;
            await unitOfWork.OrderRepository.AddAsync(order);
            return await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Sipariş kaydedilirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> UpdateAsync(int id, Order order)
    {
        ArgumentNullException.ThrowIfNull(order);
        ValidateOrder(order);

        var existing = await unitOfWork.OrderRepository.FindAsync(id);
        if (existing is null) return false;

        try
        {
            mapper.Map(order, existing);
            existing.UpdatedDate = DateTime.Now;
            await unitOfWork.OrderRepository.UpdateAsync(existing);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Sipariş güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var order = await unitOfWork.OrderRepository.FindAsync(id);
        if (order is null) return false;

        try
        {
            // Soft Delete
            order.Deleted = true;
            order.UpdatedDate = DateTime.Now;
            await unitOfWork.OrderRepository.UpdateAsync(order);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Sipariş silinirken bir hata oluştu.", ex);
        }
    }

    #endregion

    #region Custom Business Methods

    public async Task<List<Order>> GetOrdersByCustomerAsync(int customerId)
    {
        if (customerId <= 0)
            throw new ArgumentException("Geçerli bir müşteri ID'si gereklidir.", nameof(customerId));

        return await unitOfWork.OrderRepository.GetAllAsync(o =>
            o.CustomerId == customerId && !o.Deleted);
    }

    public async Task<List<Order>> GetOrdersBySupplierAsync(int supplierId)
    {
        if (supplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi ID'si gereklidir.", nameof(supplierId));

        return await unitOfWork.OrderRepository.GetAllAsync(o =>
            o.SupplierId == supplierId && !o.Deleted);
    }

    public async Task<List<Order>> GetOrdersByStatusAsync(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Sipariş durumu gereklidir.", nameof(status));

        return await unitOfWork.OrderRepository.GetAllAsync(o =>
            o.OrderStatus == status && !o.Deleted);
    }

    public async Task<List<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
            throw new ArgumentException("Başlangıç tarihi bitiş tarihinden sonra olamaz.", nameof(startDate));

        return await unitOfWork.OrderRepository.GetAllAsync(o =>
            o.OrderDate >= startDate && o.OrderDate <= endDate && !o.Deleted);
    }

    public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Sipariş durumu gereklidir.", nameof(status));

        var order = await unitOfWork.OrderRepository.FindAsync(orderId);
        if (order is null) return false;

        try
        {
            order.OrderStatus = status;
            order.UpdatedDate = DateTime.Now;
            await unitOfWork.OrderRepository.UpdateAsync(order);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Sipariş durumu güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> AssignShippingAsync(int orderId, int shippingId)
    {
        if (shippingId <= 0)
            throw new ArgumentException("Geçerli bir kargo ID'si gereklidir.", nameof(shippingId));

        var order = await unitOfWork.OrderRepository.FindAsync(orderId);
        if (order is null) return false;

        try
        {
            order.ShippingId = shippingId;
            order.OrderStatus = OrderStatuses.Shipped;
            order.UpdatedDate = DateTime.Now;
            await unitOfWork.OrderRepository.UpdateAsync(order);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Kargo ataması yapılırken bir hata oluştu.", ex);
        }
    }

    public async Task<List<Order>> GetPendingOrdersAsync()
    {
        return await unitOfWork.OrderRepository.GetAllAsync(o =>
            o.OrderStatus == OrderStatuses.Pending && !o.Deleted);
    }

    public async Task<List<Order>> GetCompletedOrdersAsync()
    {
        return await unitOfWork.OrderRepository.GetAllAsync(o =>
            o.OrderStatus == OrderStatuses.Completed && !o.Deleted);
    }

    public async Task<List<Order>> GetOrdersByFiltersAsync(
        int? customerId = null,
        int? supplierId = null,
        int? shippingId = null,
        string? status = null,
        DateTime? startDate = null,
        DateTime? endDate = null)
    {
        return await unitOfWork.OrderRepository.GetAllAsync(o =>
            !o.Deleted &&
            (!customerId.HasValue || o.CustomerId == customerId.Value) &&
            (!supplierId.HasValue || o.SupplierId == supplierId.Value) &&
            (!shippingId.HasValue || o.ShippingId == shippingId.Value) &&
            (string.IsNullOrEmpty(status) || o.OrderStatus == status) &&
            (!startDate.HasValue || o.OrderDate >= startDate.Value) &&
            (!endDate.HasValue || o.OrderDate <= endDate.Value));
    }

    #endregion

    #region Validation

    private static void ValidateOrder(Order order)
    {
        if (order.CustomerId <= 0)
            throw new ArgumentException("Geçerli bir müşteri seçilmelidir.", nameof(order));

        if (order.SupplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi seçilmelidir.", nameof(order));

        if (order.SupplierMaterialId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi malzeme seçilmelidir.", nameof(order));
    }

    #endregion
}