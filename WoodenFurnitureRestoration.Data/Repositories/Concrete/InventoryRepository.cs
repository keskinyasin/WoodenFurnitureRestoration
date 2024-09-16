using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Data.DbContextt;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;
using Microsoft.EntityFrameworkCore;

namespace WoodenFurnitureRestoration.Data.Repositories.Concrete
{
    public class InventoryRepository : Repository<Inventory>, IInventoryRepository
    {

        public InventoryRepository(WoodenFurnitureRestorationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Inventory>> GetInventoriesByConditionAsync(Expression<Func<Inventory, bool>> expression)
        {
            return await _context.Inventories
                .Where(expression)
                .ToListAsync();
        }
    }
}
