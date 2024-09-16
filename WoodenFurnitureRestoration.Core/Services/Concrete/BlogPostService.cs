using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.DbContextt;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Concrete;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete
{
    public class BlogPostService : Service<BlogPost>, IBlogPostRepository, IBlogPostService
    {
        private readonly WoodenFurnitureRestorationContext _context;
        private readonly BlogPostRepository _blogPostRepository;

        public BlogPostService(BlogPostRepository blogPostRepository, WoodenFurnitureRestorationContext context) : base(blogPostRepository, context)
        {
            _blogPostRepository = blogPostRepository;
            _context = context;
        }

        public async Task<List<BlogPost>> GetBlogPostByInvoiceAsync(int Id)
        {
            return await _blogPostRepository.GetBlogPostByInvoiceAsync(Id);
        }

        public async Task<List<BlogPost>> GetBlogPostsByCategoryAsync(int Id)
        {
            return await _blogPostRepository.GetBlogPostsByCategoryAsync(Id);
        }

        public async Task<List<BlogPost>> GetBlogPostsByConditionAsync(Expression<Func<BlogPost, bool>> expression)
        {
            return await _blogPostRepository.GetBlogPostsByConditionAsync(expression);
        }

        public async Task<List<BlogPost>> GetBlogPostsByLocationAsync(string District, string City, string Country)
        {
            return await _blogPostRepository.GetBlogPostsByLocationAsync(District, City, Country);
        }

        public async Task<List<BlogPost>> GetBlogPostsByOrderAsync(int Id)
        {
            return await _blogPostRepository.GetBlogPostsByOrderAsync(Id);
        }

        public async Task<List<BlogPost>> GetBlogPostsByProductNameAsync(string ProductName)
        {
            return await _blogPostRepository.GetBlogPostsByProductNameAsync(ProductName);
        }

        public async Task<List<BlogPost>> GetBlogPostsByRestorationNameAsync(string RestorationName)
        {
            return await _blogPostRepository.GetBlogPostsByRestorationNameAsync(RestorationName);
        }

        public async Task<List<BlogPost>> GetBlogPostsByRestorationServiceNameAsync(string RestorationServiceName)
        {
            return await _blogPostRepository.GetBlogPostsByRestorationServiceNameAsync(RestorationServiceName);
        }

        public async Task<List<BlogPost>> GetBlogPostsByReviewRatingAsync(int Rating)
        {
            return await _blogPostRepository.GetBlogPostsByReviewRatingAsync(Rating);
        }

        public async Task<List<BlogPost>> GetBlogPostsByShippingAsync(DateTime? shippingDate, ShippingType? shippingType, ShippingStatus? shippingStatus)
        {
            return await _blogPostRepository.GetBlogPostsByShippingAsync(shippingDate, shippingType, shippingStatus);
        }

        public async Task<List<BlogPost>> GetBlogPostsByTagNameAsync(string Name)
        {
            return await _blogPostRepository.GetBlogPostsByTagNameAsync(Name);
        }

        public async Task<List<BlogPost>> GetBlogPostsByFiltersAsync(bool? status = null, DateTime? date = null, DateTime? startDate = null, DateTime? endDate = null, string title = null, string content = null, int? userId = null, int? categoryId = null, int? tagId = null)
        {
            return await _blogPostRepository.GetBlogPostsByFiltersAsync(status, date, startDate, endDate, title, content, userId, categoryId, tagId);
        }
    }
}
