using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract;

public interface ICategoryService : IService<Category>
{
    Task<List<Category>> GetCategoriesByFiltersAsync(
        bool? isActive = null,
        string? name = null,
        string? description = null);
    Task<List<Category>> GetCategoriesBySupplierAsync(int supplierId);
    Task<List<Category>> GetCategoriesByCityAsync(string city);
}