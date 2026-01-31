using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class OrderDetailService(IUnitOfWork unitOfWork, IMapper mapper) : IOrderDetailService
{
    #region CRUD Operations

    public async Task<List<OrderDetail>> GetAllAsync()
    {
        return await unitOfWork.OrderDetailRepository.GetAllAsync(od => !od.Deleted);
    }

    public async Task<OrderDetail?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Geçerli bir sipariş detayı ID'si gereklidir.", nameof(id));

        return await unitOfWork.OrderDetailRepository.FindAsync(id);
    }

    public async Task<int> CreateAsync(OrderDetail orderDetail)
    {
        ArgumentNullException.ThrowIfNull(orderDetail);
        ValidateOrderDetail(orderDetail);

        try
        {
            orderDetail.CreatedDate = DateTime.Now;
            orderDetail.UpdatedDate = DateTime.Now;
            orderDetail.Deleted = false;
            await unitOfWork.OrderDetailRepository.AddAsync(orderDetail);
            return await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Sipariş detayı kaydedilirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> UpdateAsync(int id, OrderDetail orderDetail)
    {
        ArgumentNullException.ThrowIfNull(orderDetail);
        ValidateOrderDetail(orderDetail);

        var existing = await unitOfWork.OrderDetailRepository.FindAsync(id);
        if (existing is null) return false;

        try
        {
            mapper.Map(orderDetail, existing);
            existing.UpdatedDate = DateTime.Now;
            await unitOfWork.OrderDetailRepository.UpdateAsync(existing);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Sipariş detayı güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var orderDetail = await unitOfWork.OrderDetailRepository.FindAsync(id);
        if (orderDetail is null) return false;

        try
        {
            // Soft Delete
            orderDetail.Deleted = true;
            orderDetail.UpdatedDate = DateTime.Now;
            await unitOfWork.OrderDetailRepository.UpdateAsync(orderDetail);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Sipariş detayı silinirken bir hata oluştu.", ex);
        }
    }

    #endregion

    #region Custom Business Methods

    public async Task<List<OrderDetail>> GetOrderDetailsByOrderAsync(int orderId)
    {
        if (orderId <= 0)
            throw new ArgumentException("Geçerli bir sipariş ID'si gereklidir.", nameof(orderId));

        return await unitOfWork.OrderDetailRepository.GetAllAsync(od =>
            od.OrderId == orderId && !od.Deleted);
    }

    public async Task<List<OrderDetail>> GetOrderDetailsByProductAsync(int productId)
    {
        if (productId <= 0)
            throw new ArgumentException("Geçerli bir ürün ID'si gereklidir.", nameof(productId));

        return await unitOfWork.OrderDetailRepository.GetAllAsync(od =>
            od.ProductId == productId && !od.Deleted);
    }

    public async Task<List<OrderDetail>> GetOrderDetailsByRestorationAsync(int restorationId)
    {
        if (restorationId <= 0)
            throw new ArgumentException("Geçerli bir restorasyon ID'si gereklidir.", nameof(restorationId));

        return await unitOfWork.OrderDetailRepository.GetAllAsync(od =>
            od.RestorationId == restorationId && !od.Deleted);
    }

    public async Task<decimal> GetOrderTotalAsync(int orderId)
    {
        if (orderId <= 0)
            throw new ArgumentException("Geçerli bir sipariş ID'si gereklidir.", nameof(orderId));

        var orderDetails = await unitOfWork.OrderDetailRepository.GetAllAsync(od =>
            od.OrderId == orderId && !od.Deleted);

        return orderDetails.Sum(od => od.Quantity * od.UnitPrice);
    }

    public async Task<decimal> GetLineItemTotalAsync(int orderDetailId)
    {
        if (orderDetailId <= 0)
            throw new ArgumentException("Geçerli bir sipariş detayı ID'si gereklidir.", nameof(orderDetailId));

        var orderDetail = await unitOfWork.OrderDetailRepository.FindAsync(orderDetailId);
        if (orderDetail is null)
            throw new InvalidOperationException("Sipariş detayı bulunamadı.");

        return orderDetail.Quantity * orderDetail.UnitPrice;
    }

    public async Task<bool> UpdateQuantityAsync(int orderDetailId, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Miktar 0'dan büyük olmalıdır.", nameof(quantity));

        var orderDetail = await unitOfWork.OrderDetailRepository.FindAsync(orderDetailId);
        if (orderDetail is null) return false;

        try
        {
            orderDetail.Quantity = quantity;
            orderDetail.UpdatedDate = DateTime.Now;
            await unitOfWork.OrderDetailRepository.UpdateAsync(orderDetail);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Miktar güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<List<OrderDetail>> GetOrderDetailsByFiltersAsync(
        int? orderId = null,
        int? productId = null,
        int? restorationId = null,
        int? minQuantity = null,
        int? maxQuantity = null,
        decimal? minUnitPrice = null,
        decimal? maxUnitPrice = null)
    {
        return await unitOfWork.OrderDetailRepository.GetAllAsync(od =>
            !od.Deleted &&
            (!orderId.HasValue || od.OrderId == orderId.Value) &&
            (!productId.HasValue || od.ProductId == productId.Value) &&
            (!restorationId.HasValue || od.RestorationId == restorationId.Value) &&
            (!minQuantity.HasValue || od.Quantity >= minQuantity.Value) &&
            (!maxQuantity.HasValue || od.Quantity <= maxQuantity.Value) &&
            (!minUnitPrice.HasValue || od.UnitPrice >= minUnitPrice.Value) &&
            (!maxUnitPrice.HasValue || od.UnitPrice <= maxUnitPrice.Value));
    }

    #endregion

    #region Validation

    private static void ValidateOrderDetail(OrderDetail orderDetail)
    {
        if (orderDetail.OrderId <= 0)
            throw new ArgumentException("Geçerli bir sipariş seçilmelidir.", nameof(orderDetail));

        if (orderDetail.ProductId <= 0)
            throw new ArgumentException("Geçerli bir ürün seçilmelidir.", nameof(orderDetail));

        if (orderDetail.RestorationId <= 0)
            throw new ArgumentException("Geçerli bir restorasyon seçilmelidir.", nameof(orderDetail));

        if (orderDetail.Quantity <= 0)
            throw new ArgumentException("Miktar 0'dan büyük olmalıdır.", nameof(orderDetail));

        if (orderDetail.UnitPrice <= 0)
            throw new ArgumentException("Birim fiyat 0'dan büyük olmalıdır.", nameof(orderDetail));
    }

    #endregion
}