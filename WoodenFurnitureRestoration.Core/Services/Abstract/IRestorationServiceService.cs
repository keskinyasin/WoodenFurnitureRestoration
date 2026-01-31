using WoodenFurnitureRestoration.Entities;
using RestorationServiceEntity = WoodenFurnitureRestoration.Entities.RestorationService;

namespace WoodenFurnitureRestoration.Core.Services.Abstract;

public interface IRestorationServiceService
{
    // CRUD Operations
    Task<List<RestorationServiceEntity>> GetAllAsync();
    Task<RestorationServiceEntity?> GetByIdAsync(int id);
    Task<int> CreateAsync(RestorationServiceEntity restorationService);
    Task<bool> UpdateAsync(int id, RestorationServiceEntity restorationService);
    Task<bool> DeleteAsync(int id);

    // Custom Business Methods
    Task<List<RestorationServiceEntity>> GetServicesByCategoryAsync(int categoryId);
    Task<List<RestorationServiceEntity>> GetServicesByRestorationAsync(int restorationId);
    Task<List<RestorationServiceEntity>> GetServicesByStatusAsync(string status);
    Task<List<RestorationServiceEntity>> SearchServicesByNameAsync(string name);
    Task<List<RestorationServiceEntity>> GetServicesByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    Task<List<RestorationServiceEntity>> GetServicesByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<bool> UpdateStatusAsync(int serviceId, string status);
    Task<bool> UpdatePriceAsync(int serviceId, decimal newPrice);
    Task<decimal> GetAveragePriceAsync();
    Task<List<RestorationServiceEntity>> GetActiveServicesAsync();
    Task<List<RestorationServiceEntity>> GetServicesByFiltersAsync(
        int? categoryId = null,
        int? restorationId = null,
        string? status = null,
        string? name = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        decimal? minPrice = null,
        decimal? maxPrice = null);
}