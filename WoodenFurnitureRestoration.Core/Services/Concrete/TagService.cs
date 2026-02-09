using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class TagService(IUnitOfWork unitOfWork, IMapper mapper)
    : Service<Tag>(unitOfWork), ITagService
{
    private readonly IMapper _mapper = mapper;
    protected override IRepository<Tag> Repository => unitOfWork.TagRepository;

    protected override void ValidateEntity(Tag tag)
    {
        if (string.IsNullOrWhiteSpace(tag.Name))
            throw new ArgumentException("Etiket adı gereklidir.", nameof(tag));
        if (tag.Name.Length > 100)
            throw new ArgumentException("Etiket adı 100 karakterden uzun olamaz.", nameof(tag));
    }

    public async Task<Tag?> GetTagByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Etiket adı gereklidir.", nameof(name));
        var tags = await Repository.GetAllAsync(t => t.Name == name && !t.Deleted);
        return tags.FirstOrDefault();
    }

    public async Task<List<Tag>> SearchTagsByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Etiket adı gereklidir.", nameof(name));
        return await Repository.GetAllAsync(t => t.Name.Contains(name) && !t.Deleted);
    }

    public async Task<bool> IsTagExistsAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;
        var tag = await GetTagByNameAsync(name);
        return tag is not null;
    }

    public async Task<List<Tag>> GetTagsByBlogPostAsync(int blogPostId)
    {
        if (blogPostId <= 0)
            throw new ArgumentException("Geçerli bir blog post ID'si gereklidir.", nameof(blogPostId));
        var tags = await Repository.GetAllAsync(t => !t.Deleted);
        return tags.Where(t => t.BlogPostTags.Any(bpt => bpt.BlogPostId == blogPostId)).ToList();
    }

    public async Task<List<Tag>> GetTagsByCategoryAsync(int categoryId)
    {
        if (categoryId <= 0)
            throw new ArgumentException("Geçerli bir kategori ID'si gereklidir.", nameof(categoryId));
        var tags = await Repository.GetAllAsync(t => !t.Deleted);
        return tags.Where(t => t.CategoryTags.Any(ct => ct.CategoryId == categoryId)).ToList();
    }

    public async Task<List<Tag>> GetTagsByProductAsync(int productId)
    {
        if (productId <= 0)
            throw new ArgumentException("Geçerli bir ürün ID'si gereklidir.", nameof(productId));
        var tags = await Repository.GetAllAsync(t => !t.Deleted);
        return tags.Where(t => t.ProductTags.Any(pt => pt.ProductId == productId)).ToList();
    }

    public async Task<List<Tag>> GetTagsByOrderAsync(int orderId)
    {
        if (orderId <= 0)
            throw new ArgumentException("Geçerli bir sipariş ID'si gereklidir.", nameof(orderId));
        var tags = await Repository.GetAllAsync(t => !t.Deleted);
        return tags.Where(t => t.OrderTags.Any(ot => ot.OrderId == orderId)).ToList();
    }

    public async Task<List<Tag>> GetMostUsedTagsAsync(int count)
    {
        if (count <= 0)
            throw new ArgumentException("Sayı 0'dan büyük olmalıdır.", nameof(count));
        var tags = await Repository.GetAllAsync(t => !t.Deleted);
        return tags
            .OrderByDescending(t => GetTotalUsageCount(t))
            .Take(count)
            .ToList();
    }

    public async Task<int> GetTagUsageCountAsync(int tagId)
    {
        if (tagId <= 0)
            throw new ArgumentException("Geçerli bir etiket ID'si gereklidir.", nameof(tagId));
        var tag = await Repository.FindAsync(tagId);
        if (tag is null)
            return 0;
        return GetTotalUsageCount(tag);
    }

    private static int GetTotalUsageCount(Tag tag)
    {
        return tag.BlogPostTags.Count +
               tag.CategoryTags.Count +
               tag.ProductTags.Count +
               tag.OrderTags.Count +
               tag.PaymentTags.Count +
               tag.InvoiceTags.Count +
               tag.ShippingTags.Count +
               tag.SupplierMaterialTags.Count;
    }
}