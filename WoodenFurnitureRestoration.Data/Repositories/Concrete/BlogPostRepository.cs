using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<List<BlogPost>> GetBlogPostsByConditionAsync(Expression<Func<BlogPost, bool>> expression)
        {
            return await _dbSet
                .Where(expression)
                .ToListAsync();
        }

        public async Task<List<BlogPost>> GetBlogPostsByLocationAsync(string district, string city, string country)
        {
            IQueryable<BlogPost> query = _dbSet;

            if (!string.IsNullOrEmpty(district))
            {
                query = query.Where(bp => bp.Address.District == district);
            }

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(bp => bp.Address.City == city);
            }

            if (!string.IsNullOrEmpty(country))
            {
                query = query.Where(bp => bp.Address.Country == country);
            }

            return await query.ToListAsync();
        }

        public async Task<List<BlogPost>> GetBlogPostsByCategoryAsync(int Id)
        {
            return await _dbSet
                .Where(bp => bp.CategoryId == Id)
                .ToListAsync();
        }

        public async Task<List<BlogPost>> GetBlogPostByInvoiceAsync(int Id)
        {
            return await _dbSet
                .Where(bp => bp.Shipping.InvoiceId == Id)
                .ToListAsync();
        }

        public async Task<List<BlogPost>> GetBlogPostsByOrderAsync(int Id)
        {
            return await _dbSet
                .Where(bp => bp.Shipping.OrderId == Id)
                .ToListAsync();
        }

        public async Task<List<BlogPost>> GetBlogPostsByProductNameAsync(string ProductName)
        {
            return await _dbSet
                .Where(bp => bp.BlogTitle == ProductName) // Assuming BlogTitle is used to store product names
                .ToListAsync();
        }

        public async Task<List<BlogPost>> GetBlogPostsByRestorationNameAsync(string RestorationName)
        {
            return await _dbSet
                .Where(bp => bp.Restoration.RestorationName == RestorationName)
                .ToListAsync();
        }

        public async Task<List<BlogPost>> GetBlogPostsByRestorationServiceNameAsync(string RestorationServiceName)
        {
            return await _dbSet
                .Where(bp => bp.Restoration.RestorationServices.Any(rs => rs.RestorationServiceName == RestorationServiceName))
                .ToListAsync();
        }

        public async Task<List<BlogPost>> GetBlogPostsByReviewRatingAsync(int Rating)
        {
            return await _dbSet
                .Where(bp => bp.Review.Rating == Rating)
                .ToListAsync();
        }

        public async Task<List<BlogPost>> GetBlogPostsByShippingAsync(DateTime? shippingDate, ShippingType? shippingType, ShippingStatus? shippingStatus)
        {
            IQueryable<BlogPost> query = _dbSet;

            if (shippingDate.HasValue)
            {
                query = query.Where(bp => bp.Shipping.ShippingDate == shippingDate.Value);
            }

            if (shippingType.HasValue)
            {
                query = query.Where(bp => bp.Shipping.ShippingType == shippingType.Value);
            }

            if (shippingStatus.HasValue)
            {
                query = query.Where(bp => bp.Shipping.ShippingStatus == shippingStatus.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<List<BlogPost>> GetBlogPostsByTagNameAsync(string Name)
        {
            return await _dbSet
                .Where(bp => bp.BlogTitle == Name)
                .ToListAsync();
        }

        public async Task<List<BlogPost>> GetBlogPostsByFiltersAsync(bool? status, DateTime? date, DateTime? startDate, DateTime? endDate, string title, string content, int? userId, int? categoryId, int? tagId)
        {
            throw new NotImplementedException();
        }
    }
}
