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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(WoodenFurnitureRestorationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesByFiltersAsync(bool? status, string name, string description)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Category>> GetCategoryByCondition(Expression<Func<Category, bool>> expression)
        {
            return await _dbSet
                .Where(expression)
                .ToListAsync();
        }

        public async Task<List<Category>> GetCategoryNameByCustomerAndAddress(string city)
        {
            var addresses = await _context.Addresses
                .Where(a => a.City == city)
                .ToListAsync();

            var supplierIds = addresses
                .Select(a => a.SupplierId)
                .ToList();

            var result = await _context.Categories
                .Where(c => supplierIds.Contains(c.SupplierId))
                .ToListAsync();

            return result;
        }

    }
}
