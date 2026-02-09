using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract;

public interface IInventoryService : IService<Inventory>
{
    Task<List<Inventory>> GetInventoriesByProductAsync(int productId);
    Task<List<Inventory>> GetInventoriesBySupplierMaterialAsync(int supplierMaterialId);
    Task<List<Inventory>> GetInventoriesByAddressAsync(int addressId);
    Task<List<Inventory>> GetLowStockInventoriesAsync(int threshold);
    Task<List<Inventory>> GetOutOfStockInventoriesAsync();
    Task<bool> UpdateStockQuantityAsync(int id, int quantity);
    Task<List<Inventory>> GetInventoriesByFiltersAsync(
        int? productId = null,
        int? supplierMaterialId = null,
        int? addressId = null,
        int? minQuantity = null,
        int? maxQuantity = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        DateTime? startDate = null,
        DateTime? endDate = null);
}