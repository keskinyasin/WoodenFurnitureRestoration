using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class ShippingService(IUnitOfWork unitOfWork, IMapper mapper) : IShippingService
{
    #region CRUD Operations

    public async Task<List<Shipping>> GetAllAsync()
    {
        return await unitOfWork.ShippingRepository.GetAllAsync(s => !s.Deleted);
    }

    public async Task<Shipping?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Geçerli bir kargo ID'si gereklidir.", nameof(id));

        return await unitOfWork.ShippingRepository.FindAsync(id);
    }

    public async Task<int> CreateAsync(Shipping shipping)
    {
        ArgumentNullException.ThrowIfNull(shipping);
        ValidateShipping(shipping);

        try
        {
            shipping.CreatedDate = DateTime.Now;
            shipping.UpdatedDate = DateTime.Now;
            shipping.Deleted = false;
            shipping.ShippingStatus = ShippingStatus.Pending;
            await unitOfWork.ShippingRepository.AddAsync(shipping);
            return await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Kargo kaydedilirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> UpdateAsync(int id, Shipping shipping)
    {
        ArgumentNullException.ThrowIfNull(shipping);
        ValidateShipping(shipping);

        var existing = await unitOfWork.ShippingRepository.FindAsync(id);
        if (existing is null) return false;

        try
        {
            mapper.Map(shipping, existing);
            existing.UpdatedDate = DateTime.Now;
            await unitOfWork.ShippingRepository.UpdateAsync(existing);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Kargo güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var shipping = await unitOfWork.ShippingRepository.FindAsync(id);
        if (shipping is null) return false;

        try
        {
            // Soft Delete
            shipping.Deleted = true;
            shipping.UpdatedDate = DateTime.Now;
            await unitOfWork.ShippingRepository.UpdateAsync(shipping);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Kargo silinirken bir hata oluştu.", ex);
        }
    }

    #endregion

    #region Custom Business Methods

    public async Task<List<Shipping>> GetShippingsByOrderAsync(int orderId)
    {
        if (orderId <= 0)
            throw new ArgumentException("Geçerli bir sipariş ID'si gereklidir.", nameof(orderId));

        return await unitOfWork.ShippingRepository.GetAllAsync(s =>
            s.OrderId == orderId && !s.Deleted);
    }

    public async Task<List<Shipping>> GetShippingsByAddressAsync(int addressId)
    {
        if (addressId <= 0)
            throw new ArgumentException("Geçerli bir adres ID'si gereklidir.", nameof(addressId));

        return await unitOfWork.ShippingRepository.GetAllAsync(s =>
            s.AddressId == addressId && !s.Deleted);
    }

    public async Task<List<Shipping>> GetShippingsBySupplierAsync(int supplierId)
    {
        if (supplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi ID'si gereklidir.", nameof(supplierId));

        return await unitOfWork.ShippingRepository.GetAllAsync(s =>
            s.SupplierId == supplierId && !s.Deleted);
    }

    public async Task<List<Shipping>> GetShippingsByStatusAsync(ShippingStatus status)
    {
        return await unitOfWork.ShippingRepository.GetAllAsync(s =>
            s.ShippingStatus == status && !s.Deleted);
    }

    public async Task<List<Shipping>> GetShippingsByTypeAsync(ShippingType type)
    {
        return await unitOfWork.ShippingRepository.GetAllAsync(s =>
            s.ShippingType == type && !s.Deleted);
    }

    public async Task<List<Shipping>> GetShippingsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
            throw new ArgumentException("Başlangıç tarihi bitiş tarihinden sonra olamaz.", nameof(startDate));

        return await unitOfWork.ShippingRepository.GetAllAsync(s =>
            s.ShippingDate >= startDate && s.ShippingDate <= endDate && !s.Deleted);
    }

    public async Task<bool> UpdateStatusAsync(int shippingId, ShippingStatus status)
    {
        var shipping = await unitOfWork.ShippingRepository.FindAsync(shippingId);
        if (shipping is null) return false;

        try
        {
            shipping.ShippingStatus = status;
            shipping.UpdatedDate = DateTime.Now;
            await unitOfWork.ShippingRepository.UpdateAsync(shipping);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Kargo durumu güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> MarkAsShippedAsync(int shippingId)
    {
        return await UpdateStatusAsync(shippingId, ShippingStatus.Shipped);
    }

    public async Task<bool> MarkAsDeliveredAsync(int shippingId)
    {
        return await UpdateStatusAsync(shippingId, ShippingStatus.Delivered);
    }

    public async Task<bool> CancelShippingAsync(int shippingId)
    {
        return await UpdateStatusAsync(shippingId, ShippingStatus.Cancelled);
    }

    public async Task<List<Shipping>> GetPendingShippingsAsync()
    {
        return await unitOfWork.ShippingRepository.GetAllAsync(s =>
            s.ShippingStatus == ShippingStatus.Pending && !s.Deleted);
    }

    public async Task<List<Shipping>> GetDeliveredShippingsAsync()
    {
        return await unitOfWork.ShippingRepository.GetAllAsync(s =>
            s.ShippingStatus == ShippingStatus.Delivered && !s.Deleted);
    }

    public async Task<decimal> GetTotalShippingCostByOrderAsync(int orderId)
    {
        if (orderId <= 0)
            throw new ArgumentException("Geçerli bir sipariş ID'si gereklidir.", nameof(orderId));

        var shippings = await unitOfWork.ShippingRepository.GetAllAsync(s =>
            s.OrderId == orderId && !s.Deleted);

        return shippings.Sum(s => s.ShippingCost);
    }

    public async Task<List<Shipping>> GetShippingsByFiltersAsync(
        int? orderId = null,
        int? addressId = null,
        int? supplierId = null,
        ShippingStatus? status = null,
        ShippingType? type = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        decimal? minCost = null,
        decimal? maxCost = null)
    {
        return await unitOfWork.ShippingRepository.GetAllAsync(s =>
            !s.Deleted &&
            (!orderId.HasValue || s.OrderId == orderId.Value) &&
            (!addressId.HasValue || s.AddressId == addressId.Value) &&
            (!supplierId.HasValue || s.SupplierId == supplierId.Value) &&
            (!status.HasValue || s.ShippingStatus == status.Value) &&
            (!type.HasValue || s.ShippingType == type.Value) &&
            (!startDate.HasValue || s.ShippingDate >= startDate.Value) &&
            (!endDate.HasValue || s.ShippingDate <= endDate.Value) &&
            (!minCost.HasValue || s.ShippingCost >= minCost.Value) &&
            (!maxCost.HasValue || s.ShippingCost <= maxCost.Value));
    }

    #endregion

    #region Validation

    private static void ValidateShipping(Shipping shipping)
    {
        if (shipping.ShippingDate == default)
            throw new ArgumentException("Teslimat tarihi gereklidir.", nameof(shipping));

        if (shipping.ShippingCost <= 0)
            throw new ArgumentException("Teslimat ücreti 0'dan büyük olmalıdır.", nameof(shipping));

        if (shipping.OrderId <= 0)
            throw new ArgumentException("Geçerli bir sipariş seçilmelidir.", nameof(shipping));

        if (shipping.AddressId <= 0)
            throw new ArgumentException("Geçerli bir adres seçilmelidir.", nameof(shipping));

        if (shipping.SupplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi seçilmelidir.", nameof(shipping));

        if (shipping.SupplierMaterialId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi malzeme seçilmelidir.", nameof(shipping));
    }

    #endregion
}