using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using RestorationServiceEntity = WoodenFurnitureRestoration.Entities.RestorationService;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class RestorationServiceService(IUnitOfWork unitOfWork, IMapper mapper) : IRestorationServiceService
{
    // Hizmet Durumları
    private static class ServiceStatuses
    {
        public const string Active = "Aktif";
        public const string Inactive = "Pasif";
        public const string Pending = "Beklemede";
        public const string Completed = "Tamamlandı";
        public const string Cancelled = "İptal Edildi";
    }

    #region CRUD Operations

    public async Task<List<RestorationServiceEntity>> GetAllAsync()
    {
        return await unitOfWork.RestorationServiceRepository.GetAllAsync(rs => !rs.Deleted);
    }

    public async Task<RestorationServiceEntity?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Geçerli bir hizmet ID'si gereklidir.", nameof(id));

        return await unitOfWork.RestorationServiceRepository.FindAsync(id);
    }

    public async Task<int> CreateAsync(RestorationServiceEntity restorationService)
    {
        ArgumentNullException.ThrowIfNull(restorationService);
        ValidateRestorationService(restorationService);

        try
        {
            restorationService.CreatedDate = DateTime.Now;
            restorationService.UpdatedDate = DateTime.Now;
            restorationService.Deleted = false;
            restorationService.RestorationServiceStatus = ServiceStatuses.Active;
            await unitOfWork.RestorationServiceRepository.AddAsync(restorationService);
            return await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Restorasyon hizmeti kaydedilirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> UpdateAsync(int id, RestorationServiceEntity restorationService)
    {
        ArgumentNullException.ThrowIfNull(restorationService);
        ValidateRestorationService(restorationService);

        var existing = await unitOfWork.RestorationServiceRepository.FindAsync(id);
        if (existing is null) return false;

        try
        {
            mapper.Map(restorationService, existing);
            existing.UpdatedDate = DateTime.Now;
            await unitOfWork.RestorationServiceRepository.UpdateAsync(existing);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Restorasyon hizmeti güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var restorationService = await unitOfWork.RestorationServiceRepository.FindAsync(id);
        if (restorationService is null) return false;

        try
        {
            // Soft Delete
            restorationService.Deleted = true;
            restorationService.UpdatedDate = DateTime.Now;
            await unitOfWork.RestorationServiceRepository.UpdateAsync(restorationService);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Restorasyon hizmeti silinirken bir hata oluştu.", ex);
        }
    }

    #endregion

    #region Custom Business Methods

    public async Task<List<RestorationServiceEntity>> GetServicesByCategoryAsync(int categoryId)
    {
        if (categoryId <= 0)
            throw new ArgumentException("Geçerli bir kategori ID'si gereklidir.", nameof(categoryId));

        return await unitOfWork.RestorationServiceRepository.GetAllAsync(rs =>
            rs.CategoryId == categoryId && !rs.Deleted);
    }

    public async Task<List<RestorationServiceEntity>> GetServicesByRestorationAsync(int restorationId)
    {
        if (restorationId <= 0)
            throw new ArgumentException("Geçerli bir restorasyon ID'si gereklidir.", nameof(restorationId));

        return await unitOfWork.RestorationServiceRepository.GetAllAsync(rs =>
            rs.RestorationId == restorationId && !rs.Deleted);
    }

    public async Task<List<RestorationServiceEntity>> GetServicesByStatusAsync(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Hizmet durumu gereklidir.", nameof(status));

        return await unitOfWork.RestorationServiceRepository.GetAllAsync(rs =>
            rs.RestorationServiceStatus == status && !rs.Deleted);
    }

    public async Task<List<RestorationServiceEntity>> SearchServicesByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Hizmet adı gereklidir.", nameof(name));

        return await unitOfWork.RestorationServiceRepository.GetAllAsync(rs =>
            rs.RestorationServiceName.Contains(name) && !rs.Deleted);
    }

    public async Task<List<RestorationServiceEntity>> GetServicesByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        if (minPrice < 0)
            throw new ArgumentException("Minimum fiyat 0'dan küçük olamaz.", nameof(minPrice));

        if (maxPrice < minPrice)
            throw new ArgumentException("Maksimum fiyat minimum fiyattan küçük olamaz.", nameof(maxPrice));

        return await unitOfWork.RestorationServiceRepository.GetAllAsync(rs =>
            rs.RestorationServicePrice >= minPrice &&
            rs.RestorationServicePrice <= maxPrice &&
            !rs.Deleted);
    }

    public async Task<List<RestorationServiceEntity>> GetServicesByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
            throw new ArgumentException("Başlangıç tarihi bitiş tarihinden sonra olamaz.", nameof(startDate));

        return await unitOfWork.RestorationServiceRepository.GetAllAsync(rs =>
            rs.RestorationServiceDate >= startDate &&
            rs.RestorationServiceDate <= endDate &&
            !rs.Deleted);
    }

    public async Task<bool> UpdateStatusAsync(int serviceId, string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Hizmet durumu gereklidir.", nameof(status));

        var restorationService = await unitOfWork.RestorationServiceRepository.FindAsync(serviceId);
        if (restorationService is null) return false;

        try
        {
            restorationService.RestorationServiceStatus = status;
            restorationService.UpdatedDate = DateTime.Now;
            await unitOfWork.RestorationServiceRepository.UpdateAsync(restorationService);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Hizmet durumu güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> UpdatePriceAsync(int serviceId, decimal newPrice)
    {
        if (newPrice <= 0)
            throw new ArgumentException("Fiyat 0'dan büyük olmalıdır.", nameof(newPrice));

        var restorationService = await unitOfWork.RestorationServiceRepository.FindAsync(serviceId);
        if (restorationService is null) return false;

        try
        {
            restorationService.RestorationServicePrice = newPrice;
            restorationService.UpdatedDate = DateTime.Now;
            await unitOfWork.RestorationServiceRepository.UpdateAsync(restorationService);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Fiyat güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<decimal> GetAveragePriceAsync()
    {
        var services = await unitOfWork.RestorationServiceRepository.GetAllAsync(rs => !rs.Deleted);

        if (services.Count == 0)
            return 0;

        return services.Average(rs => rs.RestorationServicePrice);
    }

    public async Task<List<RestorationServiceEntity>> GetActiveServicesAsync()
    {
        return await unitOfWork.RestorationServiceRepository.GetAllAsync(rs =>
            rs.RestorationServiceStatus == ServiceStatuses.Active && !rs.Deleted);
    }

    public async Task<List<RestorationServiceEntity>> GetServicesByFiltersAsync(
        int? categoryId = null,
        int? restorationId = null,
        string? status = null,
        string? name = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        decimal? minPrice = null,
        decimal? maxPrice = null)
    {
        return await unitOfWork.RestorationServiceRepository.GetAllAsync(rs =>
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

    #endregion

    #region Validation

    private static void ValidateRestorationService(RestorationServiceEntity restorationService)
    {
        if (string.IsNullOrWhiteSpace(restorationService.RestorationServiceName))
            throw new ArgumentException("Hizmet adı gereklidir.", nameof(restorationService));

        if (restorationService.RestorationServiceName.Length > 50)
            throw new ArgumentException("Hizmet adı 50 karakterden uzun olamaz.", nameof(restorationService));

        if (string.IsNullOrWhiteSpace(restorationService.RestorationServiceDescription))
            throw new ArgumentException("Hizmet açıklaması gereklidir.", nameof(restorationService));

        if (restorationService.RestorationServiceDescription.Length > 500)
            throw new ArgumentException("Hizmet açıklaması 500 karakterden uzun olamaz.", nameof(restorationService));

        if (restorationService.RestorationServicePrice <= 0)
            throw new ArgumentException("Hizmet fiyatı 0'dan büyük olmalıdır.", nameof(restorationService));

        if (restorationService.RestorationServiceDate == default)
            throw new ArgumentException("Hizmet tarihi gereklidir.", nameof(restorationService));

        if (restorationService.CategoryId <= 0)
            throw new ArgumentException("Geçerli bir kategori seçilmelidir.", nameof(restorationService));

        if (restorationService.RestorationId <= 0)
            throw new ArgumentException("Geçerli bir restorasyon seçilmelidir.", nameof(restorationService));
    }

    #endregion
}