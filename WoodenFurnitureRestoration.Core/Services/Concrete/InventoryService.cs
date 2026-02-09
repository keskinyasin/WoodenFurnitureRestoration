using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class InventoryService(IUnitOfWork unitOfWork, IMapper mapper)
    : Service<Inventory>(unitOfWork), IInventoryService
{
    private readonly IMapper _mapper = mapper;
    protected override IRepository<Inventory> Repository => unitOfWork.InventoryRepository;

    protected override void ValidateEntity(Inventory inventory)
    {
        if (inventory.QuantityInStock < 0)
            throw new ArgumentException("Stok miktarı 0'dan küçük olamaz.", nameof(inventory));
        if (inventory.Price <= 0)
            throw new ArgumentException("Fiyat 0'dan büyük olmalıdır.", nameof(inventory));
        if (inventory.ProductId <= 0)
            throw new ArgumentException("Geçerli bir ürün seçilmelidir.", nameof(inventory));
        if (inventory.SupplierMaterialId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi malzeme seçilmelidir.", nameof(inventory));
        if (inventory.AddressId <= 0)
            throw new ArgumentException("Geçerli bir adres seçilmelidir.", nameof(inventory));
        if (inventory.InventoryDate == default)
            throw new ArgumentException("Envanter tarihi gereklidir.", nameof(inventory));
    }

    public async Task<List<Inventory>> GetInventoriesByProductAsync(int productId)
    {
        if (productId <= 0)
            throw new ArgumentException("Geçerli bir ürün ID'si gereklidir.", nameof(productId));
        return await Repository.GetAllAsync(i => i.ProductId == productId && !i.Deleted);
    }

    public async Task<List<Inventory>> GetInventoriesBySupplierMaterialAsync(int supplierMaterialId)
    {
        if (supplierMaterialId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi malzeme ID'si gereklidir.", nameof(supplierMaterialId));
        return await Repository.GetAllAsync(i => i.SupplierMaterialId == supplierMaterialId && !i.Deleted);
    }

    public async Task<List<Inventory>> GetInventoriesByAddressAsync(int addressId)
    {
        if (addressId <= 0)
            throw new ArgumentException("Geçerli bir adres ID'si gereklidir.", nameof(addressId));
        return await Repository.GetAllAsync(i => i.AddressId == addressId && !i.Deleted);
    }

    public async Task<List<Inventory>> GetLowStockInventoriesAsync(int threshold)
    {
        if (threshold < 0)
            throw new ArgumentException("Eşik değeri 0'dan küçük olamaz.", nameof(threshold));
        return await Repository.GetAllAsync(i => i.QuantityInStock <= threshold && i.QuantityInStock > 0 && !i.Deleted);
    }

    public async Task<List<Inventory>> GetOutOfStockInventoriesAsync()
    {
        return await Repository.GetAllAsync(i => i.QuantityInStock == 0 && !i.Deleted);
    }

    public async Task<bool> UpdateStockQuantityAsync(int id, int quantity)
    {
        if (quantity < 0)
            throw new ArgumentException("Stok miktarı 0'dan küçük olamaz.", nameof(quantity));
        var inventory = await Repository.FindAsync(id);
        if (inventory is null) return false;

        inventory.QuantityInStock = quantity;
        inventory.TotalAmount = quantity * inventory.Price;
        inventory.LastUpdate = DateTime.Now;
        inventory.UpdatedDate = DateTime.Now;
        await Repository.UpdateAsync(inventory);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<List<Inventory>> GetInventoriesByFiltersAsync(
        int? productId = null,
        int? supplierMaterialId = null,
        int? addressId = null,
        int? minQuantity = null,
        int? maxQuantity = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        DateTime? startDate = null,
        DateTime? endDate = null)
    {
        return await Repository.GetAllAsync(i =>
            !i.Deleted &&
            (!productId.HasValue || i.ProductId == productId.Value) &&
            (!supplierMaterialId.HasValue || i.SupplierMaterialId == supplierMaterialId.Value) &&
            (!addressId.HasValue || i.AddressId == addressId.Value) &&
            (!minQuantity.HasValue || i.QuantityInStock >= minQuantity.Value) &&
            (!maxQuantity.HasValue || i.QuantityInStock <= maxQuantity.Value) &&
            (!minPrice.HasValue || i.Price >= minPrice.Value) &&
            (!maxPrice.HasValue || i.Price <= maxPrice.Value) &&
            (!startDate.HasValue || i.InventoryDate >= startDate.Value) &&
            (!endDate.HasValue || i.InventoryDate <= endDate.Value));
    }
}