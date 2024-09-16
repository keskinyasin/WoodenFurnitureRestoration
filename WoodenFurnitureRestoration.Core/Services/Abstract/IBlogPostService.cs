using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract
{
    public interface IBlogPostService : IService<BlogPost>
    {
        
        Task<List<BlogPost>> GetBlogPostsByFiltersAsync(
            bool? status = null,
            DateTime? date = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string title = null,
            string content = null,
            int? userId = null,
            int? categoryId = null,
            int? tagId = null
        );
    }
}
