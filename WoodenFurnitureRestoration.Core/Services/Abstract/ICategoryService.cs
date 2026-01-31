using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract;

public interface ICategoryService
{
    // CRUD Operations
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<int> CreateAsync(Category category);
    Task<bool> UpdateAsync(int id, Category category);
    Task<bool> DeleteAsync(int id);

    // Custom Business Methods
    Task<List<Category>> GetCategoriesByFiltersAsync(
        bool? isActive = null,
        string? name = null,
        string? description = null);
    Task<List<Category>> GetCategoriesBySupplierAsync(int supplierId);
    Task<List<Category>> GetCategoriesByCityAsync(string city);
}