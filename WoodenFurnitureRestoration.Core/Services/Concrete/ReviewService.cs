using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class ReviewService(IUnitOfWork unitOfWork, IMapper mapper) : IReviewService
{
    // Yorum Durumları
    private static class ReviewStatuses
    {
        public const string Pending = "Beklemede";
        public const string Approved = "Onaylandı";
        public const string Rejected = "Reddedildi";
    }

    #region CRUD Operations

    public async Task<List<Review>> GetAllAsync()
    {
        return await unitOfWork.ReviewRepository.GetAllAsync(r => !r.Deleted);
    }

    public async Task<Review?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Geçerli bir yorum ID'si gereklidir.", nameof(id));

        return await unitOfWork.ReviewRepository.FindAsync(id);
    }

    public async Task<int> CreateAsync(Review review)
    {
        ArgumentNullException.ThrowIfNull(review);
        ValidateReview(review);

        try
        {
            review.CreatedDate = DateTime.Now;
            review.UpdatedDate = DateTime.Now;
            review.ReviewDate = DateTime.Now;
            review.Deleted = false;
            review.ReviewStatus = ReviewStatuses.Pending;
            await unitOfWork.ReviewRepository.AddAsync(review);
            return await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Yorum kaydedilirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> UpdateAsync(int id, Review review)
    {
        ArgumentNullException.ThrowIfNull(review);
        ValidateReview(review);

        var existing = await unitOfWork.ReviewRepository.FindAsync(id);
        if (existing is null) return false;

        try
        {
            mapper.Map(review, existing);
            existing.UpdatedDate = DateTime.Now;
            await unitOfWork.ReviewRepository.UpdateAsync(existing);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Yorum güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var review = await unitOfWork.ReviewRepository.FindAsync(id);
        if (review is null) return false;

        try
        {
            // Soft Delete
            review.Deleted = true;
            review.UpdatedDate = DateTime.Now;
            await unitOfWork.ReviewRepository.UpdateAsync(review);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Yorum silinirken bir hata oluştu.", ex);
        }
    }

    #endregion

    #region Custom Business Methods

    public async Task<List<Review>> GetReviewsByCustomerAsync(int customerId)
    {
        if (customerId <= 0)
            throw new ArgumentException("Geçerli bir müşteri ID'si gereklidir.", nameof(customerId));

        return await unitOfWork.ReviewRepository.GetAllAsync(r =>
            r.CustomerId == customerId && !r.Deleted);
    }

    public async Task<List<Review>> GetReviewsByProductAsync(int productId)
    {
        if (productId <= 0)
            throw new ArgumentException("Geçerli bir ürün ID'si gereklidir.", nameof(productId));

        return await unitOfWork.ReviewRepository.GetAllAsync(r =>
            r.ProductId == productId && !r.Deleted);
    }

    public async Task<List<Review>> GetReviewsBySupplierAsync(int supplierId)
    {
        if (supplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi ID'si gereklidir.", nameof(supplierId));

        return await unitOfWork.ReviewRepository.GetAllAsync(r =>
            r.SupplierId == supplierId && !r.Deleted);
    }

    public async Task<List<Review>> GetReviewsByRestorationAsync(int restorationId)
    {
        if (restorationId <= 0)
            throw new ArgumentException("Geçerli bir restorasyon ID'si gereklidir.", nameof(restorationId));

        return await unitOfWork.ReviewRepository.GetAllAsync(r =>
            r.RestorationId == restorationId && !r.Deleted);
    }

    public async Task<List<Review>> GetReviewsByRatingAsync(int rating)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentException("Puan 1 ile 5 arasında olmalıdır.", nameof(rating));

        return await unitOfWork.ReviewRepository.GetAllAsync(r =>
            r.Rating == rating && !r.Deleted);
    }

    public async Task<List<Review>> GetReviewsByStatusAsync(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Yorum durumu gereklidir.", nameof(status));

        return await unitOfWork.ReviewRepository.GetAllAsync(r =>
            r.ReviewStatus == status && !r.Deleted);
    }

    public async Task<List<Review>> GetReviewsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
            throw new ArgumentException("Başlangıç tarihi bitiş tarihinden sonra olamaz.", nameof(startDate));

        return await unitOfWork.ReviewRepository.GetAllAsync(r =>
            r.ReviewDate >= startDate && r.ReviewDate <= endDate && !r.Deleted);
    }

    public async Task<bool> UpdateStatusAsync(int reviewId, string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Yorum durumu gereklidir.", nameof(status));

        var review = await unitOfWork.ReviewRepository.FindAsync(reviewId);
        if (review is null) return false;

        try
        {
            review.ReviewStatus = status;
            review.UpdatedDate = DateTime.Now;
            await unitOfWork.ReviewRepository.UpdateAsync(review);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Yorum durumu güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> ApproveReviewAsync(int reviewId)
    {
        return await UpdateStatusAsync(reviewId, ReviewStatuses.Approved);
    }

    public async Task<bool> RejectReviewAsync(int reviewId)
    {
        return await UpdateStatusAsync(reviewId, ReviewStatuses.Rejected);
    }

    public async Task<double> GetAverageRatingByProductAsync(int productId)
    {
        if (productId <= 0)
            throw new ArgumentException("Geçerli bir ürün ID'si gereklidir.", nameof(productId));

        var reviews = await unitOfWork.ReviewRepository.GetAllAsync(r =>
            r.ProductId == productId &&
            r.ReviewStatus == ReviewStatuses.Approved &&
            !r.Deleted);

        if (reviews.Count == 0)
            return 0;

        return reviews.Average(r => r.Rating);
    }

    public async Task<double> GetAverageRatingBySupplierAsync(int supplierId)
    {
        if (supplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi ID'si gereklidir.", nameof(supplierId));

        var reviews = await unitOfWork.ReviewRepository.GetAllAsync(r =>
            r.SupplierId == supplierId &&
            r.ReviewStatus == ReviewStatuses.Approved &&
            !r.Deleted);

        if (reviews.Count == 0)
            return 0;

        return reviews.Average(r => r.Rating);
    }

    public async Task<List<Review>> GetPendingReviewsAsync()
    {
        return await unitOfWork.ReviewRepository.GetAllAsync(r =>
            r.ReviewStatus == ReviewStatuses.Pending && !r.Deleted);
    }

    public async Task<List<Review>> GetApprovedReviewsAsync()
    {
        return await unitOfWork.ReviewRepository.GetAllAsync(r =>
            r.ReviewStatus == ReviewStatuses.Approved && !r.Deleted);
    }

    public async Task<List<Review>> GetReviewsByFiltersAsync(
        int? customerId = null,
        int? productId = null,
        int? supplierId = null,
        int? restorationId = null,
        int? rating = null,
        string? status = null,
        DateTime? startDate = null,
        DateTime? endDate = null)
    {
        return await unitOfWork.ReviewRepository.GetAllAsync(r =>
            !r.Deleted &&
            (!customerId.HasValue || r.CustomerId == customerId.Value) &&
            (!productId.HasValue || r.ProductId == productId.Value) &&
            (!supplierId.HasValue || r.SupplierId == supplierId.Value) &&
            (!restorationId.HasValue || r.RestorationId == restorationId.Value) &&
            (!rating.HasValue || r.Rating == rating.Value) &&
            (string.IsNullOrEmpty(status) || r.ReviewStatus == status) &&
            (!startDate.HasValue || r.ReviewDate >= startDate.Value) &&
            (!endDate.HasValue || r.ReviewDate <= endDate.Value));
    }

    #endregion

    #region Validation

    private static void ValidateReview(Review review)
    {
        if (review.CustomerId <= 0)
            throw new ArgumentException("Geçerli bir müşteri seçilmelidir.", nameof(review));

        if (review.ProductId <= 0)
            throw new ArgumentException("Geçerli bir ürün seçilmelidir.", nameof(review));

        if (review.SupplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi seçilmelidir.", nameof(review));

        if (review.SupplierMaterialId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi malzeme seçilmelidir.", nameof(review));

        if (review.RestorationId <= 0)
            throw new ArgumentException("Geçerli bir restorasyon seçilmelidir.", nameof(review));

        if (string.IsNullOrWhiteSpace(review.ReviewDescription))
            throw new ArgumentException("Yorum açıklaması gereklidir.", nameof(review));

        if (review.ReviewDescription.Length > 500)
            throw new ArgumentException("Yorum açıklaması 500 karakterden uzun olamaz.", nameof(review));

        if (review.Rating < 1 || review.Rating > 5)
            throw new ArgumentException("Puan 1 ile 5 arasında olmalıdır.", nameof(review));
    }

    #endregion
}