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
        /// Kategoriye göre blog yazılarını getirir
        /// </summary>
        public async Task<List<BlogPost>> GetBlogPostsByCategoryAsync(int Id)
        {
            return await _dbSet
                .Where(bp => bp.CategoryId == Id)
                .ToListAsync();
        }

        /// <summary>
        /// Başlığa göre blog yazılarını getirir
        /// </summary>
        public async Task<List<BlogPost>> GetBlogPostsByProductNameAsync(string ProductName)
        {
            return await _dbSet
                .Where(bp => bp.BlogTitle == ProductName)
                .ToListAsync();
        }

        /// <summary>
        /// Etiket adına göre blog yazılarını getirir
        /// </summary>
        public async Task<List<BlogPost>> GetBlogPostsByTagNameAsync(string Name)
        {
            return await _dbSet
                .Include(bp => bp.BlogPostTags)
                .ThenInclude(bpt => bpt.Tag)
                .Where(bp => bp.BlogPostTags.Any(bpt => bpt.Tag.Name.Contains(Name)))
                .ToListAsync();
        }
    }
}