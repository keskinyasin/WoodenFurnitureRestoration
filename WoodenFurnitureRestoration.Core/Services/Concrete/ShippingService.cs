using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class ShippingService(IUnitOfWork unitOfWork, IMapper mapper)
    : Service<Shipping>(unitOfWork), IShippingService
{
    private readonly IMapper _mapper = mapper;
    protected override IRepository<Shipping> Repository => unitOfWork.ShippingRepository;

    protected override void ValidateEntity(Shipping shipping)
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

    public async Task<List<Shipping>> GetShippingsByOrderAsync(int orderId)
    {
        if (orderId <= 0)
            throw new ArgumentException("Geçerli bir sipariş ID'si gereklidir.", nameof(orderId));
        return await Repository.GetAllAsync(s => s.OrderId == orderId && !s.Deleted);
    }

    public async Task<List<Shipping>> GetShippingsByAddressAsync(int addressId)
    {
        if (addressId <= 0)
            throw new ArgumentException("Geçerli bir adres ID'si gereklidir.", nameof(addressId));
        return await Repository.GetAllAsync(s => s.AddressId == addressId && !s.Deleted);
    }

    public async Task<List<Shipping>> GetShippingsBySupplierAsync(int supplierId)
    {
        if (supplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi ID'si gereklidir.", nameof(supplierId));
        return await Repository.GetAllAsync(s => s.SupplierId == supplierId && !s.Deleted);
    }

    public async Task<List<Shipping>> GetShippingsByStatusAsync(ShippingStatus status)
    {
        return await Repository.GetAllAsync(s => s.ShippingStatus == status && !s.Deleted);
    }

    public async Task<List<Shipping>> GetShippingsByTypeAsync(ShippingType type)
    {
        return await Repository.GetAllAsync(s => s.ShippingType == type && !s.Deleted);
    }

    public async Task<List<Shipping>> GetShippingsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
            throw new ArgumentException("Başlangıç tarihi bitiş tarihinden sonra olamaz.", nameof(startDate));
        return await Repository.GetAllAsync(s => s.ShippingDate >= startDate && s.ShippingDate <= endDate && !s.Deleted);
    }

    public async Task<bool> UpdateStatusAsync(int shippingId, ShippingStatus status)
    {
        var shipping = await Repository.FindAsync(shippingId);
        if (shipping is null) return false;

        shipping.ShippingStatus = status;
        shipping.UpdatedDate = DateTime.Now;
        await Repository.UpdateAsync(shipping);
        await unitOfWork.SaveChangesAsync();
        return true;
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
        return await Repository.GetAllAsync(s => s.ShippingStatus == ShippingStatus.Pending && !s.Deleted);
    }

    public async Task<List<Shipping>> GetDeliveredShippingsAsync()
    {
        return await Repository.GetAllAsync(s => s.ShippingStatus == ShippingStatus.Delivered && !s.Deleted);
    }

    public async Task<decimal> GetTotalShippingCostByOrderAsync(int orderId)
    {
        if (orderId <= 0)
            throw new ArgumentException("Geçerli bir sipariş ID'si gereklidir.", nameof(orderId));
        var shippings = await Repository.GetAllAsync(s => s.OrderId == orderId && !s.Deleted);
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
        return await Repository.GetAllAsync(s =>
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
}