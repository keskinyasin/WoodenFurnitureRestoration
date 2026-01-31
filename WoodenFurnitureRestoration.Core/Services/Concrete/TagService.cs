using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class TagService(IUnitOfWork unitOfWork, IMapper mapper) : ITagService
{
    #region CRUD Operations

    public async Task<List<Tag>> GetAllAsync()
    {
        return await unitOfWork.TagRepository.GetAllAsync(t => !t.Deleted);
    }

    public async Task<Tag?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Geçerli bir etiket ID'si gereklidir.", nameof(id));

        return await unitOfWork.TagRepository.FindAsync(id);
    }

    public async Task<int> CreateAsync(Tag tag)
    {
        ArgumentNullException.ThrowIfNull(tag);
        ValidateTag(tag);

        // İsim benzersizlik kontrolü
        if (await IsTagExistsAsync(tag.Name))
            throw new InvalidOperationException("Bu etiket adı zaten mevcut.");

        try
        {
            tag.CreatedDate = DateTime.Now;
            tag.UpdatedDate = DateTime.Now;
            tag.Deleted = false;
            await unitOfWork.TagRepository.AddAsync(tag);
            return await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Etiket kaydedilirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> UpdateAsync(int id, Tag tag)
    {
        ArgumentNullException.ThrowIfNull(tag);
        ValidateTag(tag);

        var existing = await unitOfWork.TagRepository.FindAsync(id);
        if (existing is null) return false;

        // İsim değiştiyse benzersizlik kontrolü
        if (existing.Name != tag.Name && await IsTagExistsAsync(tag.Name))
            throw new InvalidOperationException("Bu etiket adı başka bir etiket tarafından kullanılıyor.");

        try
        {
            mapper.Map(tag, existing);
            existing.UpdatedDate = DateTime.Now;
            await unitOfWork.TagRepository.UpdateAsync(existing);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Etiket güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var tag = await unitOfWork.TagRepository.FindAsync(id);
        if (tag is null) return false;

        try
        {
            // Soft Delete
            tag.Deleted = true;
            tag.UpdatedDate = DateTime.Now;
            await unitOfWork.TagRepository.UpdateAsync(tag);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Etiket silinirken bir hata oluştu.", ex);
        }
    }

    #endregion

    #region Custom Business Methods

    public async Task<Tag?> GetTagByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Etiket adı gereklidir.", nameof(name));

        var tags = await unitOfWork.TagRepository.GetAllAsync(t =>
            t.Name == name && !t.Deleted);

        return tags.FirstOrDefault();
    }

    public async Task<List<Tag>> SearchTagsByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Etiket adı gereklidir.", nameof(name));

        return await unitOfWork.TagRepository.GetAllAsync(t =>
            t.Name.Contains(name) && !t.Deleted);
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

        var tags = await unitOfWork.TagRepository.GetAllAsync(t => !t.Deleted);
        return tags.Where(t => t.BlogPostTags.Any(bpt => bpt.BlogPostId == blogPostId)).ToList();
    }

    public async Task<List<Tag>> GetTagsByCategoryAsync(int categoryId)
    {
        if (categoryId <= 0)
            throw new ArgumentException("Geçerli bir kategori ID'si gereklidir.", nameof(categoryId));

        var tags = await unitOfWork.TagRepository.GetAllAsync(t => !t.Deleted);
        return tags.Where(t => t.CategoryTags.Any(ct => ct.CategoryId == categoryId)).ToList();
    }

    public async Task<List<Tag>> GetTagsByProductAsync(int productId)
    {
        if (productId <= 0)
            throw new ArgumentException("Geçerli bir ürün ID'si gereklidir.", nameof(productId));

        var tags = await unitOfWork.TagRepository.GetAllAsync(t => !t.Deleted);
        return tags.Where(t => t.ProductTags.Any(pt => pt.ProductId == productId)).ToList();
    }

    public async Task<List<Tag>> GetTagsByOrderAsync(int orderId)
    {
        if (orderId <= 0)
            throw new ArgumentException("Geçerli bir sipariş ID'si gereklidir.", nameof(orderId));

        var tags = await unitOfWork.TagRepository.GetAllAsync(t => !t.Deleted);
        return tags.Where(t => t.OrderTags.Any(ot => ot.OrderId == orderId)).ToList();
    }

    public async Task<List<Tag>> GetMostUsedTagsAsync(int count)
    {
        if (count <= 0)
            throw new ArgumentException("Sayı 0'dan büyük olmalıdır.", nameof(count));

        var tags = await unitOfWork.TagRepository.GetAllAsync(t => !t.Deleted);

        return tags
            .OrderByDescending(t => GetTotalUsageCount(t))
            .Take(count)
            .ToList();
    }

    public async Task<int> GetTagUsageCountAsync(int tagId)
    {
        if (tagId <= 0)
            throw new ArgumentException("Geçerli bir etiket ID'si gereklidir.", nameof(tagId));

        var tag = await unitOfWork.TagRepository.FindAsync(tagId);
        if (tag is null)
            return 0;

        return GetTotalUsageCount(tag);
    }

    #endregion

    #region Private Methods

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

    #endregion

    #region Validation

    private static void ValidateTag(Tag tag)
    {
        if (string.IsNullOrWhiteSpace(tag.Name))
            throw new ArgumentException("Etiket adı gereklidir.", nameof(tag));

        if (tag.Name.Length > 100)
            throw new ArgumentException("Etiket adı 100 karakterden uzun olamaz.", nameof(tag));
    }

    #endregion
}