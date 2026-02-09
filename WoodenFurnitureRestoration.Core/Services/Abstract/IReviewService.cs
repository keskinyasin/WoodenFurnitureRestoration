using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract;

public interface IReviewService : IService<Review>
{
    Task<List<Review>> GetReviewsByCustomerAsync(int customerId);
    Task<List<Review>> GetReviewsByProductAsync(int productId);
    Task<List<Review>> GetReviewsBySupplierAsync(int supplierId);
    Task<List<Review>> GetReviewsByRestorationAsync(int restorationId);
    Task<List<Review>> GetReviewsByRatingAsync(int rating);
    Task<List<Review>> GetReviewsByStatusAsync(string status);
    Task<List<Review>> GetReviewsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<bool> UpdateStatusAsync(int reviewId, string status);
    Task<bool> ApproveReviewAsync(int reviewId);
    Task<bool> RejectReviewAsync(int reviewId);
    Task<double> GetAverageRatingByProductAsync(int productId);
    Task<double> GetAverageRatingBySupplierAsync(int supplierId);
    Task<List<Review>> GetPendingReviewsAsync();
    Task<List<Review>> GetApprovedReviewsAsync();
    Task<List<Review>> GetReviewsByFiltersAsync(
        int? customerId = null,
        int? productId = null,
        int? supplierId = null,
        int? restorationId = null,
        int? rating = null,
        string? status = null,
        DateTime? startDate = null,
        DateTime? endDate = null);
}