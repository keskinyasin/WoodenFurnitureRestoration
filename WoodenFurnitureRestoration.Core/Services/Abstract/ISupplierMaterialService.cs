using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract;

public interface ISupplierMaterialService : IService<SupplierMaterial>
{
    Task<List<SupplierMaterial>> GetMaterialsBySupplierAsync(int supplierId);
    Task<List<SupplierMaterial>> GetMaterialsByCategoryAsync(int categoryId);
    Task<List<SupplierMaterial>> GetMaterialsByAddressAsync(int addressId);
    Task<List<SupplierMaterial>> SearchMaterialsByNameAsync(string name);
    Task<List<SupplierMaterial>> GetMaterialsByStatusAsync(string status);
    Task<List<SupplierMaterial>> GetMaterialsByTypeAsync(string type);
    Task<List<SupplierMaterial>> GetMaterialsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    Task<bool> UpdatePriceAsync(int materialId, decimal newPrice);
    Task<bool> UpdateStatusAsync(int materialId, string status);
    Task<bool> UpdateQuantityAsync(int materialId, int quantity);
    Task<decimal> GetAveragePriceAsync();
    Task<List<SupplierMaterial>> GetLowStockMaterialsAsync(int threshold);
    Task<List<SupplierMaterial>> GetMaterialsByFiltersAsync(
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
        int? maxQuantity = null);
}