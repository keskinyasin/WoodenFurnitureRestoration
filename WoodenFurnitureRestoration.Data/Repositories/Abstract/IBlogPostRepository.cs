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
        // ✅ SADECE GEÇERLİ CUSTOM METODLAR
        Task<List<BlogPost>> GetBlogPostsByCategoryAsync(int Id);
        Task<List<BlogPost>> GetBlogPostsByProductNameAsync(string ProductName);
        Task<List<BlogPost>> GetBlogPostsByTagNameAsync(string Name);
    }
}