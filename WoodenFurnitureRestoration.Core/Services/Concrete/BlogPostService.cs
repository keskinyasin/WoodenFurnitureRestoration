using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class BlogPostService(IUnitOfWork unitOfWork, IMapper mapper) : IBlogPostService
{
    #region CRUD Operations

    public async Task<List<BlogPost>> GetAllAsync()
    {
        return await unitOfWork.BlogPostRepository.GetAllAsync();
    }

    public async Task<BlogPost?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Geçerli bir blog post ID'si gereklidir.", nameof(id));

        return await unitOfWork.BlogPostRepository.FindAsync(id);
    }

    public async Task<int> CreateAsync(BlogPost blogPost)
    {
        ArgumentNullException.ThrowIfNull(blogPost);
        ValidateBlogPost(blogPost);

        try
        {
            blogPost.CreatedDate = DateTime.Now;
            blogPost.UpdatedDate = DateTime.Now;
            await unitOfWork.BlogPostRepository.AddAsync(blogPost);
            return await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Blog post kaydedilirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> UpdateAsync(int id, BlogPost blogPost)
    {
        ArgumentNullException.ThrowIfNull(blogPost);
        ValidateBlogPost(blogPost);

        var existing = await unitOfWork.BlogPostRepository.FindAsync(id);
        if (existing is null) return false;

        try
        {
            mapper.Map(blogPost, existing);
            existing.UpdatedDate = DateTime.Now;
            await unitOfWork.BlogPostRepository.UpdateAsync(existing);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Blog post güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var blogPost = await unitOfWork.BlogPostRepository.FindAsync(id);
        if (blogPost is null) return false;

        try
        {
            await unitOfWork.BlogPostRepository.DeleteAsync(blogPost);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Blog post silinirken bir hata oluştu.", ex);
        }
    }

    #endregion

    #region Custom Business Methods

    public async Task<List<BlogPost>> GetBlogPostsByCategoryAsync(int categoryId)
    {
        if (categoryId <= 0)
            throw new ArgumentException("Geçerli bir kategori ID'si gereklidir.", nameof(categoryId));

        return await unitOfWork.BlogPostRepository.GetBlogPostsByCategoryAsync(categoryId);
    }

    public async Task<List<BlogPost>> GetBlogPostsByProductNameAsync(string productName)
    {
        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Ürün adı gereklidir.", nameof(productName));

        return await unitOfWork.BlogPostRepository.GetBlogPostsByProductNameAsync(productName);
    }

    public async Task<List<BlogPost>> GetBlogPostsByTagNameAsync(string tagName)
    {
        if (string.IsNullOrWhiteSpace(tagName))
            throw new ArgumentException("Etiket adı gereklidir.", nameof(tagName));

        return await unitOfWork.BlogPostRepository.GetBlogPostsByTagNameAsync(tagName);
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
        return await unitOfWork.BlogPostRepository.GetAllAsync(bp =>
            !bp.Deleted &&
            (!publishedDate.HasValue || bp.PublishedDate.Date == publishedDate.Value.Date) &&
            (!startDate.HasValue || bp.PublishedDate >= startDate.Value) &&
            (!endDate.HasValue || bp.PublishedDate <= endDate.Value) &&
            (string.IsNullOrEmpty(title) || bp.BlogTitle.Contains(title)) &&
            (string.IsNullOrEmpty(content) || bp.BlogContent.Contains(content)) &&
            (!customerId.HasValue || bp.CustomerId == customerId.Value) &&
            (!categoryId.HasValue || bp.CategoryId == categoryId.Value));
    }

    #endregion

    #region Validation

    private static void ValidateBlogPost(BlogPost blogPost)
    {
        if (string.IsNullOrWhiteSpace(blogPost.BlogTitle))
            throw new ArgumentException("Blog başlığı gereklidir.", nameof(blogPost));

        if (string.IsNullOrWhiteSpace(blogPost.BlogContent))
            throw new ArgumentException("Blog içeriği gereklidir.", nameof(blogPost));

        if (blogPost.BlogTitle.Length > 255)
            throw new ArgumentException("Blog başlığı 255 karakterden uzun olamaz.", nameof(blogPost));

        if (blogPost.CategoryId <= 0)
            throw new ArgumentException("Geçerli bir kategori seçilmelidir.", nameof(blogPost));
    }

    #endregion
}