using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Data.Repositories.Abstract
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        // Base IRepository<OrderDetail>'de tüm gerekli metodlar zaten var
        // Custom OrderDetail-specific metodlar gerekirse buraya eklenebilir
    }
}