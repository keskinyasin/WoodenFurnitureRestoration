using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract;

public interface IBlogPostService : IService<BlogPost>
{
    Task<List<BlogPost>> GetBlogPostsByCategoryAsync(int categoryId);
    Task<List<BlogPost>> GetBlogPostsByProductNameAsync(string productName);
    Task<List<BlogPost>> GetBlogPostsByTagNameAsync(string tagName);
    Task<List<BlogPost>> GetBlogPostsByFiltersAsync(
        DateTime? publishedDate = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        string? title = null,
        string? content = null,
        int? customerId = null,
        int? categoryId = null);
}