using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

// FIX: Use the correct RestorationService entity type from WoodenFurnitureRestoration.Entities namespace
public class RestorationServiceService(IUnitOfWork unitOfWork, IMapper mapper)
    : Service<WoodenFurnitureRestoration.Entities.RestorationService>(unitOfWork), IRestorationServiceService
{
    private readonly IMapper _mapper = mapper;
    protected override IRepository<WoodenFurnitureRestoration.Entities.RestorationService> Repository => unitOfWork.RestorationServiceRepository;

    // Hizmet Durumları
    private static class ServiceStatuses
    {
        public const string Active = "Aktif";
        public const string Inactive = "Pasif";
        public const string Pending = "Beklemede";
        public const string Completed = "Tamamlandı";
        public const string Cancelled = "İptal Edildi";
    }

    protected override void ValidateEntity(WoodenFurnitureRestoration.Entities.RestorationService entity)
    {
        if (string.IsNullOrWhiteSpace(entity.RestorationServiceName))
            throw new ArgumentException("Hizmet adı gereklidir.", nameof(entity));
        if (entity.RestorationServiceName.Length > 50)
            throw new ArgumentException("Hizmet adı 50 karakterden uzun olamaz.", nameof(entity));
        if (string.IsNullOrWhiteSpace(entity.RestorationServiceDescription))
            throw new ArgumentException("Hizmet açıklaması gereklidir.", nameof(entity));
        if (entity.RestorationServiceDescription.Length > 500)
            throw new ArgumentException("Hizmet açıklaması 500 karakterden uzun olamaz.", nameof(entity));
        if (entity.RestorationServicePrice <= 0)
            throw new ArgumentException("Hizmet fiyatı 0'dan büyük olmalıdır.", nameof(entity));
        if (entity.RestorationServiceDate == default)
            throw new ArgumentException("Hizmet tarihi gereklidir.", nameof(entity));
        if (entity.CategoryId <= 0)
            throw new ArgumentException("Geçerli bir kategori seçilmelidir.", nameof(entity));
        if (entity.RestorationId <= 0)
            throw new ArgumentException("Geçerli bir restorasyon seçilmelidir.", nameof(entity));
    }

    public async Task<List<WoodenFurnitureRestoration.Entities.RestorationService>> GetServicesByCategoryAsync(int categoryId)
    {
        if (categoryId <= 0)
            throw new ArgumentException("Geçerli bir kategori ID'si gereklidir.", nameof(categoryId));
        return await Repository.GetAllAsync(rs => rs.CategoryId == categoryId && !rs.Deleted);
    }

    public async Task<List<WoodenFurnitureRestoration.Entities.RestorationService>> GetServicesByRestorationAsync(int restorationId)
    {
        if (restorationId <= 0)
            throw new ArgumentException("Geçerli bir restorasyon ID'si gereklidir.", nameof(restorationId));
        return await Repository.GetAllAsync(rs => rs.RestorationId == restorationId && !rs.Deleted);
    }

    public async Task<List<WoodenFurnitureRestoration.Entities.RestorationService>> GetServicesByStatusAsync(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Hizmet durumu gereklidir.", nameof(status));
        return await Repository.GetAllAsync(rs => rs.RestorationServiceStatus == status && !rs.Deleted);
    }

    public async Task<List<WoodenFurnitureRestoration.Entities.RestorationService>> SearchServicesByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Hizmet adı gereklidir.", nameof(name));
        return await Repository.GetAllAsync(rs => rs.RestorationServiceName.Contains(name) && !rs.Deleted);
    }

    public async Task<List<WoodenFurnitureRestoration.Entities.RestorationService>> GetServicesByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        if (minPrice < 0)
            throw new ArgumentException("Minimum fiyat 0'dan küçük olamaz.", nameof(minPrice));
        if (maxPrice < minPrice)
            throw new ArgumentException("Maksimum fiyat minimum fiyattan küçük olamaz.", nameof(maxPrice));
        return await Repository.GetAllAsync(rs => rs.RestorationServicePrice >= minPrice && rs.RestorationServicePrice <= maxPrice && !rs.Deleted);
    }

    public async Task<List<WoodenFurnitureRestoration.Entities.RestorationService>> GetServicesByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
            throw new ArgumentException("Başlangıç tarihi bitiş tarihinden sonra olamaz.", nameof(startDate));
        return await Repository.GetAllAsync(rs => rs.RestorationServiceDate >= startDate && rs.RestorationServiceDate <= endDate && !rs.Deleted);
    }

    public async Task<bool> UpdateStatusAsync(int serviceId, string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Hizmet durumu gereklidir.", nameof(status));
        var restorationService = await Repository.FindAsync(serviceId);
        if (restorationService is null) return false;

        restorationService.RestorationServiceStatus = status;
        restorationService.UpdatedDate = DateTime.Now;
        await Repository.UpdateAsync(restorationService);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdatePriceAsync(int serviceId, decimal newPrice)
    {
        if (newPrice <= 0)
            throw new ArgumentException("Fiyat 0'dan büyük olmalıdır.", nameof(newPrice));
        var restorationService = await Repository.FindAsync(serviceId);
        if (restorationService is null) return false;

        restorationService.RestorationServicePrice = newPrice;
        restorationService.UpdatedDate = DateTime.Now;
        await Repository.UpdateAsync(restorationService);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<decimal> GetAveragePriceAsync()
    {
        var services = await Repository.GetAllAsync(rs => !rs.Deleted);
        if (services.Count == 0)
            return 0;
        return services.Average(rs => rs.RestorationServicePrice);
    }

    public async Task<List<WoodenFurnitureRestoration.Entities.RestorationService>> GetActiveServicesAsync()
    {
        return await Repository.GetAllAsync(rs => rs.RestorationServiceStatus == ServiceStatuses.Active && !rs.Deleted);
    }

    public async Task<List<WoodenFurnitureRestoration.Entities.RestorationService>> GetServicesByFiltersAsync(
        int? categoryId = null,
        int? restorationId = null,
        string? status = null,
        string? name = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        decimal? minPrice = null,
        decimal? maxPrice = null)
    {
        return await Repository.GetAllAsync(rs =>
            !rs.Deleted &&
            (!categoryId.HasValue || rs.CategoryId == categoryId.Value) &&
            (!restorationId.HasValue || rs.RestorationId == restorationId.Value) &&
            (string.IsNullOrEmpty(status) || rs.RestorationServiceStatus == status) &&
            (string.IsNullOrEmpty(name) || rs.RestorationServiceName.Contains(name)) &&
            (!startDate.HasValue || rs.RestorationServiceDate >= startDate.Value) &&
            (!endDate.HasValue || rs.RestorationServiceDate <= endDate.Value) &&
            (!minPrice.HasValue || rs.RestorationServicePrice >= minPrice.Value) &&
            (!maxPrice.HasValue || rs.RestorationServicePrice <= maxPrice.Value));
    }
}