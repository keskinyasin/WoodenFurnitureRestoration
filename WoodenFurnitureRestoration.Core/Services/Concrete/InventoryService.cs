using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class InventoryService(IUnitOfWork unitOfWork, IMapper mapper) : IInventoryService
{
    #region CRUD Operations

    public async Task<List<Inventory>> GetAllAsync()
    {
        return await unitOfWork.InventoryRepository.GetAllAsync(i => !i.Deleted);
    }

    public async Task<Inventory?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Geçerli bir envanter ID'si gereklidir.", nameof(id));

        return await unitOfWork.InventoryRepository.FindAsync(id);
    }

    public async Task<int> CreateAsync(Inventory inventory)
    {
        ArgumentNullException.ThrowIfNull(inventory);
        ValidateInventory(inventory);

        try
        {
            inventory.CreatedDate = DateTime.Now;
            inventory.UpdatedDate = DateTime.Now;
            inventory.LastUpdate = DateTime.Now;
            inventory.Deleted = false;
            inventory.TotalAmount = inventory.QuantityInStock * inventory.Price;
            await unitOfWork.InventoryRepository.AddAsync(inventory);
            return await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Envanter kaydedilirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> UpdateAsync(int id, Inventory inventory)
    {
        ArgumentNullException.ThrowIfNull(inventory);
        ValidateInventory(inventory);

        var existing = await unitOfWork.InventoryRepository.FindAsync(id);
        if (existing is null) return false;

        try
        {
            mapper.Map(inventory, existing);
            existing.UpdatedDate = DateTime.Now;
            existing.LastUpdate = DateTime.Now;
            existing.TotalAmount = existing.QuantityInStock * existing.Price;
            await unitOfWork.InventoryRepository.UpdateAsync(existing);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Envanter güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var inventory = await unitOfWork.InventoryRepository.FindAsync(id);
        if (inventory is null) return false;

        try
        {
            // Soft Delete
            inventory.Deleted = true;
            inventory.UpdatedDate = DateTime.Now;
            await unitOfWork.InventoryRepository.UpdateAsync(inventory);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Envanter silinirken bir hata oluştu.", ex);
        }
    }

    #endregion

    #region Custom Business Methods

    public async Task<List<Inventory>> GetInventoriesByProductAsync(int productId)
    {
        if (productId <= 0)
            throw new ArgumentException("Geçerli bir ürün ID'si gereklidir.", nameof(productId));

        return await unitOfWork.InventoryRepository.GetAllAsync(i =>
            i.ProductId == productId && !i.Deleted);
    }

    public async Task<List<Inventory>> GetInventoriesBySupplierMaterialAsync(int supplierMaterialId)
    {
        if (supplierMaterialId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi malzeme ID'si gereklidir.", nameof(supplierMaterialId));

        return await unitOfWork.InventoryRepository.GetAllAsync(i =>
            i.SupplierMaterialId == supplierMaterialId && !i.Deleted);
    }

    public async Task<List<Inventory>> GetInventoriesByAddressAsync(int addressId)
    {
        if (addressId <= 0)
            throw new ArgumentException("Geçerli bir adres ID'si gereklidir.", nameof(addressId));

        return await unitOfWork.InventoryRepository.GetAllAsync(i =>
            i.AddressId == addressId && !i.Deleted);
    }

    public async Task<List<Inventory>> GetLowStockInventoriesAsync(int threshold)
    {
        if (threshold < 0)
            throw new ArgumentException("Eşik değeri 0'dan küçük olamaz.", nameof(threshold));

        return await unitOfWork.InventoryRepository.GetAllAsync(i =>
            i.QuantityInStock <= threshold && i.QuantityInStock > 0 && !i.Deleted);
    }

    public async Task<List<Inventory>> GetOutOfStockInventoriesAsync()
    {
        return await unitOfWork.InventoryRepository.GetAllAsync(i =>
            i.QuantityInStock == 0 && !i.Deleted);
    }

    public async Task<bool> UpdateStockQuantityAsync(int id, int quantity)
    {
        if (quantity < 0)
            throw new ArgumentException("Stok miktarı 0'dan küçük olamaz.", nameof(quantity));

        var inventory = await unitOfWork.InventoryRepository.FindAsync(id);
        if (inventory is null) return false;

        try
        {
            inventory.QuantityInStock = quantity;
            inventory.TotalAmount = quantity * inventory.Price;
            inventory.LastUpdate = DateTime.Now;
            inventory.UpdatedDate = DateTime.Now;
            await unitOfWork.InventoryRepository.UpdateAsync(inventory);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Stok miktarı güncellenirken bir hata oluştu.", ex);
        }
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
        return await unitOfWork.InventoryRepository.GetAllAsync(i =>
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

    #endregion

    #region Validation

    private static void ValidateInventory(Inventory inventory)
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

    #endregion
}