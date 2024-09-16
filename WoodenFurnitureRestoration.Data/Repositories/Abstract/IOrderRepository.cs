using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Data.Repositories.Abstract
{
    public interface IOrderRepository : IRepository<Order>
    {

        Task<List<Order>> GetOrdersByConditionAsync(Expression<Func<Order, bool>> expression);
                             
    }
}
