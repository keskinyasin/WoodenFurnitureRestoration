using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Data.Filters;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Data.Repositories.Abstract
{
    public interface IBlogPostRepository : IRepository<BlogPost>
    {

        Task<List<BlogPost>> GetBlogPostsByConditionAsync(Expression<Func<BlogPost, bool>> expression);

        Task<List<BlogPost>> GetBlogPostsByLocationAsync(string District, string City, string Country);
        Task<List<BlogPost>> GetBlogPostsByCategoryAsync(int Id);
        Task<List<BlogPost>> GetBlogPostByInvoiceAsync(int Id);
        Task<List<BlogPost>> GetBlogPostsByOrderAsync(int Id);
        Task<List<BlogPost>> GetBlogPostsByProductNameAsync(string ProductName);
        Task<List<BlogPost>> GetBlogPostsByRestorationNameAsync(string RestorationName);
        Task<List<BlogPost>> GetBlogPostsByRestorationServiceNameAsync(string RestorationServiceName);
        Task<List<BlogPost>> GetBlogPostsByReviewRatingAsync(int Rating);
        Task<List<BlogPost>> GetBlogPostsByShippingAsync(DateTime? shippingDate, ShippingType? shippingType, ShippingStatus? shippingStatus);
        Task<List<BlogPost>> GetBlogPostsByTagNameAsync(string Name);

    }

}
