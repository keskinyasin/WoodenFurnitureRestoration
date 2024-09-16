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
    public class CategoryService : Service<Category>, ICategoryService, ICategoryRepository
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly WoodenFurnitureRestorationContext _context;

        public CategoryService(CategoryRepository categoryRepository, WoodenFurnitureRestorationContext context) : base(categoryRepository, context)
        {
            _categoryRepository = categoryRepository;
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesByFiltersAsync(bool? status = null, string name = null, string description = null)
        {
            return await _categoryRepository.GetCategoriesByFiltersAsync(status, name, description);
        }

        public async Task<List<Category>> GetCategoryByCondition(Expression<Func<Category, bool>> expression)
        {
            return await _categoryRepository.GetCategoryByCondition(expression);
        }

        public async Task<List<Category>> GetCategoryNameByCustomerAndAddress(string city)
        {
            return await _categoryRepository.GetCategoryNameByCustomerAndAddress(city);
        }
    }
}
