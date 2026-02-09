using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class BlogPostService(IUnitOfWork unitOfWork, IMapper mapper)
    : Service<BlogPost>(unitOfWork), IBlogPostService
{
    private readonly IMapper _mapper = mapper;

    protected override IRepository<BlogPost> Repository => _unitOfWork.BlogPostRepository;

    protected override void ValidateEntity(BlogPost entity)
    {
        if (string.IsNullOrWhiteSpace(entity.BlogTitle))
            throw new ArgumentException("Blog başlığı zorunludur.");
        if (string.IsNullOrWhiteSpace(entity.BlogContent))
            throw new ArgumentException("Blog içeriği zorunludur.");
        if (entity.CategoryId <= 0)
            throw new ArgumentException("Kategori seçimi zorunludur.");
    }

    public async Task<List<BlogPost>> GetBlogPostsByCategoryAsync(int categoryId)
    {
        return await Repository.GetAllAsync(b => b.CategoryId == categoryId && !b.Deleted);
    }

    public async Task<List<BlogPost>> GetBlogPostsByCustomerAsync(int customerId)
    {
        return await Repository.GetAllAsync(b => b.CustomerId == customerId && !b.Deleted);
    }

    public async Task<List<BlogPost>> SearchBlogPostsAsync(string searchTerm)
    {
        return await Repository.GetAllAsync(b =>
            (b.BlogTitle.Contains(searchTerm) || b.BlogContent.Contains(searchTerm)) && !b.Deleted);
    }

    public async Task<List<BlogPost>> GetRecentBlogPostsAsync(int count = 10)
    {
        var posts = await Repository.GetAllAsync(b => !b.Deleted);
        return posts.OrderByDescending(b => b.PublishedDate).Take(count).ToList();
    }

    public async Task<List<BlogPost>> GetBlogPostsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await Repository.GetAllAsync(b =>
            b.PublishedDate >= startDate && b.PublishedDate <= endDate && !b.Deleted);
    }

    public async Task<List<BlogPost>> GetBlogPostsByProductNameAsync(string productName)
    {
        // Eğer BlogPost ile Product arasında doğrudan bir ilişki yoksa, başlıkta ürün adı geçen bloglar döndürülür.
        return await Repository.GetAllAsync(b =>
            b.BlogTitle.Contains(productName) && !b.Deleted);
    }

    public async Task<List<BlogPost>> GetBlogPostsByTagNameAsync(string tagName)
    {
        // BlogPost ile Tag ilişkisi BlogPostTags üzerinden
        return await Repository.GetAllAsync(b =>
            b.BlogPostTags.Any(x => x.Tag.Name == tagName) && !b.Deleted);
    }

    public async Task<List<BlogPost>> GetBlogPostsByFiltersAsync(
        DateTime? publishedDate = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        string? title = null,
        string? content = null,
        int? customerId = null,
        int? categoryId = null)
    {
        var query = await Repository.GetAllAsync(b => !b.Deleted);

        if (publishedDate.HasValue)
            query = query.Where(b => b.PublishedDate.Date == publishedDate.Value.Date).ToList();
        if (startDate.HasValue)
            query = query.Where(b => b.PublishedDate >= startDate.Value).ToList();
        if (endDate.HasValue)
            query = query.Where(b => b.PublishedDate <= endDate.Value).ToList();
        if (!string.IsNullOrWhiteSpace(title))
            query = query.Where(b => b.BlogTitle.Contains(title)).ToList();
        if (!string.IsNullOrWhiteSpace(content))
            query = query.Where(b => b.BlogContent.Contains(content)).ToList();
        if (customerId.HasValue)
            query = query.Where(b => b.CustomerId == customerId.Value).ToList();
        if (categoryId.HasValue)
            query = query.Where(b => b.CategoryId == categoryId.Value).ToList();

        return query;
    }
}