using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract;

public interface ITagService : IService<Tag>
{
    Task<Tag?> GetTagByNameAsync(string name);
    Task<List<Tag>> SearchTagsByNameAsync(string name);
    Task<bool> IsTagExistsAsync(string name);
    Task<List<Tag>> GetTagsByBlogPostAsync(int blogPostId);
    Task<List<Tag>> GetTagsByCategoryAsync(int categoryId);
    Task<List<Tag>> GetTagsByProductAsync(int productId);
    Task<List<Tag>> GetTagsByOrderAsync(int orderId);
    Task<List<Tag>> GetMostUsedTagsAsync(int count);
    Task<int> GetTagUsageCountAsync(int tagId);
}