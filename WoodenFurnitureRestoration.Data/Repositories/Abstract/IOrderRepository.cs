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
        // Base IRepository<Order>'de tüm gerekli metodlar zaten var
        // Custom Order-specific metodlar gerekirse buraya eklenebilir
    }
}