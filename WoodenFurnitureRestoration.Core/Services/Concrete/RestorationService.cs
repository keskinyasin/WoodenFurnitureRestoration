using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class RestorationService(IUnitOfWork unitOfWork, IMapper mapper)
    : Service<Restoration>(unitOfWork), IRestorationService
{
    private readonly IMapper _mapper = mapper;
    protected override IRepository<Restoration> Repository => unitOfWork.RestorationRepository;

    private static class RestorationStatuses
    {
        public const string Pending = "Beklemede";
        public const string InProgress = "Devam Ediyor";
        public const string Completed = "Tamamlandı";
        public const string Cancelled = "İptal Edildi";
        public const string OnHold = "Bekletiliyor";
    }

    protected override void ValidateEntity(Restoration restoration)
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
            restoration.RestorationImage = "https://via.placeholder.com/400x300?text=Restorasyon";
        if (string.IsNullOrWhiteSpace(restoration.RestorationStatus))
            restoration.RestorationStatus = "Pending";
        if (restoration.RestorationDate == default)
            throw new ArgumentException("Restorasyon tarihi gereklidir.", nameof(restoration));
        if (restoration.RestorationEndDate == default)
            throw new ArgumentException("Restorasyon bitiş tarihi gereklidir.", nameof(restoration));
        if (restoration.RestorationEndDate < restoration.RestorationDate)
            throw new ArgumentException("Bitiş tarihi başlangıç tarihinden önce olamaz.", nameof(restoration));
        if (restoration.CategoryId <= 0)
            throw new ArgumentException("Geçerli bir kategori seçilmelidir.", nameof(restoration));
    }

    public async Task<List<Restoration>> GetRestorationsByCategoryAsync(int categoryId)
    {
        if (categoryId <= 0)
            throw new ArgumentException("Geçerli bir kategori ID'si gereklidir.", nameof(categoryId));
        return await Repository.GetAllAsync(r => r.CategoryId == categoryId && !r.Deleted);
    }

    public async Task<List<Restoration>> GetRestorationsByStatusAsync(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Restorasyon durumu gereklidir.", nameof(status));
        return await Repository.GetAllAsync(r => r.RestorationStatus == status && !r.Deleted);
    }

    public async Task<List<Restoration>> SearchRestorationsByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Restorasyon adı gereklidir.", nameof(name));
        return await Repository.GetAllAsync(r => r.RestorationName.Contains(name) && !r.Deleted);
    }

    public async Task<List<Restoration>> GetRestorationsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
            throw new ArgumentException("Başlangıç tarihi bitiş tarihinden sonra olamaz.", nameof(startDate));
        return await Repository.GetAllAsync(r => r.RestorationDate >= startDate && r.RestorationDate <= endDate && !r.Deleted);
    }

    public async Task<List<Restoration>> GetRestorationsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        if (minPrice < 0)
            throw new ArgumentException("Minimum fiyat 0'dan küçük olamaz.", nameof(minPrice));
        if (maxPrice < minPrice)
            throw new ArgumentException("Maksimum fiyat minimum fiyattan küçük olamaz.", nameof(maxPrice));
        return await Repository.GetAllAsync(r => r.RestorationPrice >= minPrice && r.RestorationPrice <= maxPrice && !r.Deleted);
    }

    public async Task<bool> UpdateStatusAsync(int restorationId, string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Restorasyon durumu gereklidir.", nameof(status));
        var restoration = await Repository.FindAsync(restorationId);
        if (restoration is null) return false;

        restoration.RestorationStatus = status;
        restoration.UpdatedDate = DateTime.Now;
        if (status == RestorationStatuses.Completed)
            restoration.RestorationEndDate = DateTime.Now;
        await Repository.UpdateAsync(restoration);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExtendEndDateAsync(int restorationId, DateTime newEndDate)
    {
        var restoration = await Repository.FindAsync(restorationId);
        if (restoration is null) return false;
        if (newEndDate <= restoration.RestorationDate)
            throw new ArgumentException("Yeni bitiş tarihi başlangıç tarihinden sonra olmalıdır.", nameof(newEndDate));

        restoration.RestorationEndDate = newEndDate;
        restoration.UpdatedDate = DateTime.Now;
        await Repository.UpdateAsync(restoration);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<List<Restoration>> GetActiveRestorationsAsync()
    {
        return await Repository.GetAllAsync(r => r.RestorationStatus == RestorationStatuses.InProgress && !r.Deleted);
    }

    public async Task<List<Restoration>> GetCompletedRestorationsAsync()
    {
        return await Repository.GetAllAsync(r => r.RestorationStatus == RestorationStatuses.Completed && !r.Deleted);
    }

    public async Task<List<Restoration>> GetOverdueRestorationsAsync()
    {
        var today = DateTime.Today;
        return await Repository.GetAllAsync(r =>
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
        return await Repository.GetAllAsync(r =>
            !r.Deleted &&
            (!categoryId.HasValue || r.CategoryId == categoryId.Value) &&
            (string.IsNullOrEmpty(status) || r.RestorationStatus == status) &&
            (string.IsNullOrEmpty(name) || r.RestorationName.Contains(name)) &&
            (!startDate.HasValue || r.RestorationDate >= startDate.Value) &&
            (!endDate.HasValue || r.RestorationDate <= endDate.Value) &&
            (!minPrice.HasValue || r.RestorationPrice >= minPrice.Value) &&
            (!maxPrice.HasValue || r.RestorationPrice <= maxPrice.Value));
    }
}