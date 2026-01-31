using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract;

public interface IBlogPostService
{
    // CRUD Operations
    Task<List<BlogPost>> GetAllAsync();
    Task<BlogPost?> GetByIdAsync(int id);
    Task<int> CreateAsync(BlogPost blogPost);
    Task<bool> UpdateAsync(int id, BlogPost blogPost);
    Task<bool> DeleteAsync(int id);

    // Custom Business Methods
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