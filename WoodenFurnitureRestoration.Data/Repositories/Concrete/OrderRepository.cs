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
    public class OrderRepository : Repository<Order>, IOrderRepository
    {

        public OrderRepository(WoodenFurnitureRestorationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByConditionAsync(Expression<Func<Order, bool>> expression)
        {
            return await _context.Orders
                .Where(expression)
                .ToListAsync();
        }
    }
}
