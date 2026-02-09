using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract;

public interface IProductService : IService<Product>
{
    Task<List<Product>> GetProductsByCategoryAsync(int categoryId);
    Task<List<Product>> GetProductsBySupplierAsync(int supplierId);
    Task<List<Product>> GetProductsBySupplierMaterialAsync(int supplierMaterialId);
    Task<List<Product>> SearchProductsByNameAsync(string productName);
    Task<List<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    Task<bool> UpdatePriceAsync(int productId, decimal newPrice);
    Task<decimal> GetAveragePriceAsync();
    Task<List<Product>> GetTopSellingProductsAsync(int count);
    Task<List<Product>> GetProductsByFiltersAsync(
        int? categoryId = null,
        int? supplierId = null,
        int? supplierMaterialId = null,
        string? productName = null,
        decimal? minPrice = null,
        decimal? maxPrice = null);
}