using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class RestorationService(IUnitOfWork unitOfWork, IMapper mapper) : IRestorationService
{
    // Restorasyon Durumları
    private static class RestorationStatuses
    {
        public const string Pending = "Beklemede";
        public const string InProgress = "Devam Ediyor";
        public const string Completed = "Tamamlandı";
        public const string Cancelled = "İptal Edildi";
        public const string OnHold = "Bekletiliyor";
    }

    #region CRUD Operations

    public async Task<List<Restoration>> GetAllAsync()
    {
        return await unitOfWork.RestorationRepository.GetAllAsync(r => !r.Deleted);
    }

    public async Task<Restoration?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Geçerli bir restorasyon ID'si gereklidir.", nameof(id));

        return await unitOfWork.RestorationRepository.FindAsync(id);
    }

    public async Task<int> CreateAsync(Restoration restoration)
    {
        ArgumentNullException.ThrowIfNull(restoration);
        ValidateRestoration(restoration);

        try
        {
            restoration.CreatedDate = DateTime.Now;
            restoration.UpdatedDate = DateTime.Now;
            restoration.Deleted = false;
            restoration.RestorationStatus = RestorationStatuses.Pending;
            await unitOfWork.RestorationRepository.AddAsync(restoration);
            return await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Restorasyon kaydedilirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> UpdateAsync(int id, Restoration restoration)
    {
        ArgumentNullException.ThrowIfNull(restoration);
        ValidateRestoration(restoration);

        var existing = await unitOfWork.RestorationRepository.FindAsync(id);
        if (existing is null) return false;

        try
        {
            mapper.Map(restoration, existing);
            existing.UpdatedDate = DateTime.Now;
            await unitOfWork.RestorationRepository.UpdateAsync(existing);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Restorasyon güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var restoration = await unitOfWork.RestorationRepository.FindAsync(id);
        if (restoration is null) return false;

        try
        {
            // Soft Delete
            restoration.Deleted = true;
            restoration.UpdatedDate = DateTime.Now;
            await unitOfWork.RestorationRepository.UpdateAsync(restoration);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Restorasyon silinirken bir hata oluştu.", ex);
        }
    }

    #endregion

    #region Custom Business Methods

    public async Task<List<Restoration>> GetRestorationsByCategoryAsync(int categoryId)
    {
        if (categoryId <= 0)
            throw new ArgumentException("Geçerli bir kategori ID'si gereklidir.", nameof(categoryId));

        return await unitOfWork.RestorationRepository.GetAllAsync(r =>
            r.CategoryId == categoryId && !r.Deleted);
    }

    public async Task<List<Restoration>> GetRestorationsByStatusAsync(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Restorasyon durumu gereklidir.", nameof(status));

        return await unitOfWork.RestorationRepository.GetAllAsync(r =>
            r.RestorationStatus == status && !r.Deleted);
    }

    public async Task<List<Restoration>> SearchRestorationsByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Restorasyon adı gereklidir.", nameof(name));

        return await unitOfWork.RestorationRepository.GetAllAsync(r =>
            r.RestorationName.Contains(name) && !r.Deleted);
    }

    public async Task<List<Restoration>> GetRestorationsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
            throw new ArgumentException("Başlangıç tarihi bitiş tarihinden sonra olamaz.", nameof(startDate));

        return await unitOfWork.RestorationRepository.GetAllAsync(r =>
            r.RestorationDate >= startDate && r.RestorationDate <= endDate && !r.Deleted);
    }

    public async Task<List<Restoration>> GetRestorationsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        if (minPrice < 0)
            throw new ArgumentException("Minimum fiyat 0'dan küçük olamaz.", nameof(minPrice));

        if (maxPrice < minPrice)
            throw new ArgumentException("Maksimum fiyat minimum fiyattan küçük olamaz.", nameof(maxPrice));

        return await unitOfWork.RestorationRepository.GetAllAsync(r =>
            r.RestorationPrice >= minPrice && r.RestorationPrice <= maxPrice && !r.Deleted);
    }

    public async Task<bool> UpdateStatusAsync(int restorationId, string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Restorasyon durumu gereklidir.", nameof(status));

        var restoration = await unitOfWork.RestorationRepository.FindAsync(restorationId);
        if (restoration is null) return false;

        try
        {
            restoration.RestorationStatus = status;
            restoration.UpdatedDate = DateTime.Now;

            // Tamamlandı durumuna geçtiyse bitiş tarihini güncelle
            if (status == RestorationStatuses.Completed)
                restoration.RestorationEndDate = DateTime.Now;

            await unitOfWork.RestorationRepository.UpdateAsync(restoration);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Restorasyon durumu güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> ExtendEndDateAsync(int restorationId, DateTime newEndDate)
    {
        var restoration = await unitOfWork.RestorationRepository.FindAsync(restorationId);
        if (restoration is null) return false;

        if (newEndDate <= restoration.RestorationDate)
            throw new ArgumentException("Yeni bitiş tarihi başlangıç tarihinden sonra olmalıdır.", nameof(newEndDate));

        try
        {
            restoration.RestorationEndDate = newEndDate;
            restoration.UpdatedDate = DateTime.Now;
            await unitOfWork.RestorationRepository.UpdateAsync(restoration);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Bitiş tarihi güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<List<Restoration>> GetActiveRestorationsAsync()
    {
        return await unitOfWork.RestorationRepository.GetAllAsync(r =>
            r.RestorationStatus == RestorationStatuses.InProgress && !r.Deleted);
    }

    public async Task<List<Restoration>> GetCompletedRestorationsAsync()
    {
        return await unitOfWork.RestorationRepository.GetAllAsync(r =>
            r.RestorationStatus == RestorationStatuses.Completed && !r.Deleted);
    }

    public async Task<List<Restoration>> GetOverdueRestorationsAsync()
    {
        var today = DateTime.Today;
        return await unitOfWork.RestorationRepository.GetAllAsync(r =>
            r.RestorationEndDate < today &&
            r.RestorationStatus != RestorationStatuses.Completed &&
            r.RestorationStatus != RestorationStatuses.Cancelled &&
            !r.Deleted);
    }

    public async Task<List<Restoration>> GetRestorationsByFiltersAsync(
        int? categoryId = null,
        string? status = null,
        string? name = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        decimal? minPrice = null,
        decimal? maxPrice = null)
    {
        return await unitOfWork.RestorationRepository.GetAllAsync(r =>
            !r.Deleted &&
            (!categoryId.HasValue || r.CategoryId == categoryId.Value) &&
            (string.IsNullOrEmpty(status) || r.RestorationStatus == status) &&
            (string.IsNullOrEmpty(name) || r.RestorationName.Contains(name)) &&
            (!startDate.HasValue || r.RestorationDate >= startDate.Value) &&
            (!endDate.HasValue || r.RestorationDate <= endDate.Value) &&
            (!minPrice.HasValue || r.RestorationPrice >= minPrice.Value) &&
            (!maxPrice.HasValue || r.RestorationPrice <= maxPrice.Value));
    }

    #endregion

    #region Validation

    private static void ValidateRestoration(Restoration restoration)
    {
        if (string.IsNullOrWhiteSpace(restoration.RestorationName))
            throw new ArgumentException("Restorasyon adı gereklidir.", nameof(restoration));

        if (restoration.RestorationName.Length > 100)
            throw new ArgumentException("Restorasyon adı 100 karakterden uzun olamaz.", nameof(restoration));

        if (string.IsNullOrWhiteSpace(restoration.RestorationDescription))
            throw new ArgumentException("Restorasyon açıklaması gereklidir.", nameof(restoration));

        if (restoration.RestorationDescription.Length > 500)
            throw new ArgumentException("Restorasyon açıklaması 500 karakterden uzun olamaz.", nameof(restoration));

        if (restoration.RestorationPrice <= 0)
            throw new ArgumentException("Restorasyon fiyatı 0'dan büyük olmalıdır.", nameof(restoration));

        if (string.IsNullOrWhiteSpace(restoration.RestorationImage))
            throw new ArgumentException("Restorasyon görseli gereklidir.", nameof(restoration));

        if (restoration.RestorationDate == default)
            throw new ArgumentException("Restorasyon tarihi gereklidir.", nameof(restoration));

        if (restoration.RestorationEndDate == default)
            throw new ArgumentException("Restorasyon bitiş tarihi gereklidir.", nameof(restoration));

        if (restoration.RestorationEndDate < restoration.RestorationDate)
            throw new ArgumentException("Bitiş tarihi başlangıç tarihinden önce olamaz.", nameof(restoration));

        if (restoration.CategoryId <= 0)
            throw new ArgumentException("Geçerli bir kategori seçilmelidir.", nameof(restoration));
    }

    #endregion
}