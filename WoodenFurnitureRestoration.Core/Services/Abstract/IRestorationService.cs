using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract;

public interface IRestorationService
{
    // CRUD Operations
    Task<List<Restoration>> GetAllAsync();
    Task<Restoration?> GetByIdAsync(int id);
    Task<int> CreateAsync(Restoration restoration);
    Task<bool> UpdateAsync(int id, Restoration restoration);
    Task<bool> DeleteAsync(int id);

    // Custom Business Methods
    Task<List<Restoration>> GetRestorationsByCategoryAsync(int categoryId);
    Task<List<Restoration>> GetRestorationsByStatusAsync(string status);
    Task<List<Restoration>> SearchRestorationsByNameAsync(string name);
    Task<List<Restoration>> GetRestorationsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<List<Restoration>> GetRestorationsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    Task<bool> UpdateStatusAsync(int restorationId, string status);
    Task<bool> ExtendEndDateAsync(int restorationId, DateTime newEndDate);
    Task<List<Restoration>> GetActiveRestorationsAsync();
    Task<List<Restoration>> GetCompletedRestorationsAsync();
    Task<List<Restoration>> GetOverdueRestorationsAsync();
    Task<List<Restoration>> GetRestorationsByFiltersAsync(
        int? categoryId = null,
        string? status = null,
        string? name = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        decimal? minPrice = null,
        decimal? maxPrice = null);
}