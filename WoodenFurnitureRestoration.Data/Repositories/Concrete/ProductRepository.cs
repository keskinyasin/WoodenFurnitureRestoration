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
    public class ProductRepository : Repository<Product> , IProductRepository
    {

        public ProductRepository(WoodenFurnitureRestorationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductByConditionAsync(Expression<Func<Product, bool>> expression)
        {
            return await _context.Products
                .Where(expression)
                .ToListAsync();
        }
    }
}
