using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WoodenFurnitureRestoration.Data.DbContextt;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Data.Repositories.Concrete
{
    public class BlogPostRepository : Repository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(WoodenFurnitureRestorationContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Verilen koşula göre blog yazılarını getirir
        /// </summary>
        public async Task<List<BlogPost>> GetBlogPostsByConditionAsync(Expression<Func<BlogPost, bool>> expression)
        {
            return await _dbSet
                .Where(expression)
                .ToListAsync();
        }

        /// <summary>
        /// Kategoriye göre blog yazılarını getirir
        /// </summary>
        public async Task<List<BlogPost>> GetBlogPostsByCategoryAsync(int Id)
        {
            return await _dbSet
                .Where(bp => bp.CategoryId == Id)
                .ToListAsync();
        }

        /// <summary>
        /// Ürün adına göre blog yazılarını getirir
        /// </summary>
        public async Task<List<BlogPost>> GetBlogPostsByProductNameAsync(string ProductName)
        {
            return await _dbSet
                .Where(bp => bp.BlogTitle == ProductName)
                .ToListAsync();
        }

        /// <summary>
        /// Başlığa göre blog yazılarını getirir
        /// </summary>
        public async Task<List<BlogPost>> GetBlogPostsByTagNameAsync(string Name)
        {
            return await _dbSet
                .Where(bp => bp.BlogTitle.Contains(Name))
                .ToListAsync();
        }

        /// <summary>
        /// Müşteriye göre blog yazılarını getirir
        /// </summary>
        public async Task<List<BlogPost>> GetBlogPostsByCustomerAsync(int customerId)
        {
            return await _dbSet
                .Where(bp => bp.CustomerId == customerId)
                .ToListAsync();
        }

        /// <summary>
        /// Yayınlanma tarihine göre blog yazılarını getirir
        /// </summary>
        public async Task<List<BlogPost>> GetBlogPostsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(bp => bp.PublishedDate >= startDate && bp.PublishedDate <= endDate)
                .ToListAsync();
        }

        public async Task<List<BlogPost>> GetBlogPostsByFiltersAsync(
            bool? status,
            DateTime? date,
            DateTime? startDate,
            DateTime? endDate,
            string? title,
            string? content,
            int? userId,
            int? categoryId,
            int? tagId)
        {
            IQueryable<BlogPost> query = _dbSet;

            if (categoryId.HasValue)
                query = query.Where(bp => bp.CategoryId == categoryId.Value);

            if (userId.HasValue)
                query = query.Where(bp => bp.CustomerId == userId.Value);

            if (!string.IsNullOrEmpty(title))
                query = query.Where(bp => bp.BlogTitle.Contains(title));

            if (!string.IsNullOrEmpty(content))
                query = query.Where(bp => bp.BlogContent.Contains(content));

            if (startDate.HasValue && endDate.HasValue)
                query = query.Where(bp => bp.PublishedDate >= startDate && bp.PublishedDate <= endDate);

            return await query.ToListAsync();
        }

        public Task<List<BlogPost>> GetBlogPostsByLocationAsync(string District, string City, string Country)
        {
            throw new NotImplementedException();
        }

        public Task<List<BlogPost>> GetBlogPostByInvoiceAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BlogPost>> GetBlogPostsByOrderAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BlogPost>> GetBlogPostsByRestorationNameAsync(string RestorationName)
        {
            throw new NotImplementedException();
        }

        public Task<List<BlogPost>> GetBlogPostsByRestorationServiceNameAsync(string RestorationServiceName)
        {
            throw new NotImplementedException();
        }

        public Task<List<BlogPost>> GetBlogPostsByReviewRatingAsync(int Rating)
        {
            throw new NotImplementedException();
        }

        public Task<List<BlogPost>> GetBlogPostsByShippingAsync(DateTime? shippingDate, ShippingType? shippingType, ShippingStatus? shippingStatus)
        {
            throw new NotImplementedException();
        }

    }
}