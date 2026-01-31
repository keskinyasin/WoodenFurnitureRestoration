using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class SupplierMaterialService(IUnitOfWork unitOfWork, IMapper mapper) : ISupplierMaterialService
{
    // Malzeme Durumları
    private static class MaterialStatuses
    {
        public const string Available = "Mevcut";
        public const string OutOfStock = "Stokta Yok";
        public const string Discontinued = "Üretimi Durdu";
        public const string OnOrder = "Sipariş Edildi";
    }

    #region CRUD Operations

    public async Task<List<SupplierMaterial>> GetAllAsync()
    {
        return await unitOfWork.SupplierMaterialRepository.GetAllAsync(sm => !sm.Deleted);
    }

    public async Task<SupplierMaterial?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Geçerli bir malzeme ID'si gereklidir.", nameof(id));

        return await unitOfWork.SupplierMaterialRepository.FindAsync(id);
    }

    public async Task<int> CreateAsync(SupplierMaterial supplierMaterial)
    {
        ArgumentNullException.ThrowIfNull(supplierMaterial);
        ValidateSupplierMaterial(supplierMaterial);

        try
        {
            supplierMaterial.CreatedDate = DateTime.Now;
            supplierMaterial.UpdatedDate = DateTime.Now;
            supplierMaterial.Deleted = false;
            supplierMaterial.MaterialStatus = MaterialStatuses.Available;
            await unitOfWork.SupplierMaterialRepository.AddAsync(supplierMaterial);
            return await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Malzeme kaydedilirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> UpdateAsync(int id, SupplierMaterial supplierMaterial)
    {
        ArgumentNullException.ThrowIfNull(supplierMaterial);
        ValidateSupplierMaterial(supplierMaterial);

        var existing = await unitOfWork.SupplierMaterialRepository.FindAsync(id);
        if (existing is null) return false;

        try
        {
            mapper.Map(supplierMaterial, existing);
            existing.UpdatedDate = DateTime.Now;
            await unitOfWork.SupplierMaterialRepository.UpdateAsync(existing);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Malzeme güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var supplierMaterial = await unitOfWork.SupplierMaterialRepository.FindAsync(id);
        if (supplierMaterial is null) return false;

        try
        {
            // Soft Delete
            supplierMaterial.Deleted = true;
            supplierMaterial.UpdatedDate = DateTime.Now;
            await unitOfWork.SupplierMaterialRepository.UpdateAsync(supplierMaterial);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Malzeme silinirken bir hata oluştu.", ex);
        }
    }

    #endregion

    #region Custom Business Methods

    public async Task<List<SupplierMaterial>> GetMaterialsBySupplierAsync(int supplierId)
    {
        if (supplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi ID'si gereklidir.", nameof(supplierId));

        return await unitOfWork.SupplierMaterialRepository.GetAllAsync(sm =>
            sm.SupplierId == supplierId && !sm.Deleted);
    }

    public async Task<List<SupplierMaterial>> GetMaterialsByCategoryAsync(int categoryId)
    {
        if (categoryId <= 0)
            throw new ArgumentException("Geçerli bir kategori ID'si gereklidir.", nameof(categoryId));

        return await unitOfWork.SupplierMaterialRepository.GetAllAsync(sm =>
            sm.CategoryId == categoryId && !sm.Deleted);
    }

    public async Task<List<SupplierMaterial>> GetMaterialsByAddressAsync(int addressId)
    {
        if (addressId <= 0)
            throw new ArgumentException("Geçerli bir adres ID'si gereklidir.", nameof(addressId));

        return await unitOfWork.SupplierMaterialRepository.GetAllAsync(sm =>
            sm.AddressId == addressId && !sm.Deleted);
    }

    public async Task<List<SupplierMaterial>> SearchMaterialsByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Malzeme adı gereklidir.", nameof(name));

        return await unitOfWork.SupplierMaterialRepository.GetAllAsync(sm =>
            sm.MaterialName.Contains(name) && !sm.Deleted);
    }

    public async Task<List<SupplierMaterial>> GetMaterialsByStatusAsync(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Malzeme durumu gereklidir.", nameof(status));

        return await unitOfWork.SupplierMaterialRepository.GetAllAsync(sm =>
            sm.MaterialStatus == status && !sm.Deleted);
    }

    public async Task<List<SupplierMaterial>> GetMaterialsByTypeAsync(string type)
    {
        if (string.IsNullOrWhiteSpace(type))
            throw new ArgumentException("Malzeme türü gereklidir.", nameof(type));

        return await unitOfWork.SupplierMaterialRepository.GetAllAsync(sm =>
            sm.MaterialType == type && !sm.Deleted);
    }

    public async Task<List<SupplierMaterial>> GetMaterialsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        if (minPrice < 0)
            throw new ArgumentException("Minimum fiyat 0'dan küçük olamaz.", nameof(minPrice));

        if (maxPrice < minPrice)
            throw new ArgumentException("Maksimum fiyat minimum fiyattan küçük olamaz.", nameof(maxPrice));

        return await unitOfWork.SupplierMaterialRepository.GetAllAsync(sm =>
            sm.MaterialPrice >= minPrice && sm.MaterialPrice <= maxPrice && !sm.Deleted);
    }

    public async Task<bool> UpdatePriceAsync(int materialId, decimal newPrice)
    {
        if (newPrice <= 0)
            throw new ArgumentException("Fiyat 0'dan büyük olmalıdır.", nameof(newPrice));

        var material = await unitOfWork.SupplierMaterialRepository.FindAsync(materialId);
        if (material is null) return false;

        try
        {
            material.MaterialPrice = newPrice;
            material.UpdatedDate = DateTime.Now;
            await unitOfWork.SupplierMaterialRepository.UpdateAsync(material);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Fiyat güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> UpdateStatusAsync(int materialId, string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Malzeme durumu gereklidir.", nameof(status));

        var material = await unitOfWork.SupplierMaterialRepository.FindAsync(materialId);
        if (material is null) return false;

        try
        {
            material.MaterialStatus = status;
            material.UpdatedDate = DateTime.Now;
            await unitOfWork.SupplierMaterialRepository.UpdateAsync(material);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Malzeme durumu güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> UpdateQuantityAsync(int materialId, int quantity)
    {
        if (quantity < 0)
            throw new ArgumentException("Miktar 0'dan küçük olamaz.", nameof(quantity));

        var material = await unitOfWork.SupplierMaterialRepository.FindAsync(materialId);
        if (material is null) return false;

        try
        {
            material.MaterialQuantity = quantity;
            material.UpdatedDate = DateTime.Now;

            // Miktar 0 ise durumu güncelle
            if (quantity == 0)
                material.MaterialStatus = MaterialStatuses.OutOfStock;

            await unitOfWork.SupplierMaterialRepository.UpdateAsync(material);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Miktar güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<decimal> GetAveragePriceAsync()
    {
        var materials = await unitOfWork.SupplierMaterialRepository.GetAllAsync(sm => !sm.Deleted);

        if (materials.Count == 0)
            return 0;

        return materials.Average(sm => sm.MaterialPrice);
    }

    public async Task<List<SupplierMaterial>> GetLowStockMaterialsAsync(int threshold)
    {
        if (threshold < 0)
            throw new ArgumentException("Eşik değeri 0'dan küçük olamaz.", nameof(threshold));

        return await unitOfWork.SupplierMaterialRepository.GetAllAsync(sm =>
            sm.MaterialQuantity <= threshold && sm.MaterialQuantity > 0 && !sm.Deleted);
    }

    public async Task<List<SupplierMaterial>> GetMaterialsByFiltersAsync(
        int? supplierId = null,
        int? categoryId = null,
        int? addressId = null,
        string? name = null,
        string? status = null,
        string? type = null,
        string? category = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        int? minQuantity = null,
        int? maxQuantity = null)
    {
        return await unitOfWork.SupplierMaterialRepository.GetAllAsync(sm =>
            !sm.Deleted &&
            (!supplierId.HasValue || sm.SupplierId == supplierId.Value) &&
            (!categoryId.HasValue || sm.CategoryId == categoryId.Value) &&
            (!addressId.HasValue || sm.AddressId == addressId.Value) &&
            (string.IsNullOrEmpty(name) || sm.MaterialName.Contains(name)) &&
            (string.IsNullOrEmpty(status) || sm.MaterialStatus == status) &&
            (string.IsNullOrEmpty(type) || sm.MaterialType == type) &&
            (string.IsNullOrEmpty(category) || sm.MaterialCategory == category) &&
            (!minPrice.HasValue || sm.MaterialPrice >= minPrice.Value) &&
            (!maxPrice.HasValue || sm.MaterialPrice <= maxPrice.Value) &&
            (!minQuantity.HasValue || sm.MaterialQuantity >= minQuantity.Value) &&
            (!maxQuantity.HasValue || sm.MaterialQuantity <= maxQuantity.Value));
    }

    #endregion

    #region Validation

    private static void ValidateSupplierMaterial(SupplierMaterial material)
    {
        if (string.IsNullOrWhiteSpace(material.MaterialName))
            throw new ArgumentException("Malzeme adı gereklidir.", nameof(material));

        if (material.MaterialName.Length > 150)
            throw new ArgumentException("Malzeme adı 150 karakterden uzun olamaz.", nameof(material));

        if (string.IsNullOrWhiteSpace(material.MaterialDescription))
            throw new ArgumentException("Malzeme açıklaması gereklidir.", nameof(material));

        if (material.MaterialDescription.Length > 500)
            throw new ArgumentException("Malzeme açıklaması 500 karakterden uzun olamaz.", nameof(material));

        if (material.MaterialPrice <= 0)
            throw new ArgumentException("Malzeme fiyatı 0'dan büyük olmalıdır.", nameof(material));

        if (material.MaterialQuantity < 0)
            throw new ArgumentException("Malzeme miktarı 0'dan küçük olamaz.", nameof(material));

        if (string.IsNullOrWhiteSpace(material.MaterialCategory))
            throw new ArgumentException("Malzeme kategorisi gereklidir.", nameof(material));

        if (string.IsNullOrWhiteSpace(material.MaterialType))
            throw new ArgumentException("Malzeme türü gereklidir.", nameof(material));

        if (material.SupplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi seçilmelidir.", nameof(material));

        if (material.CategoryId <= 0)
            throw new ArgumentException("Geçerli bir kategori seçilmelidir.", nameof(material));
    }

    #endregion
}