using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Data.Repositories.Abstract
{
    public interface IShippingRepository : IRepository<Shipping>
    {
        // Base IRepository<Shipping>'de tüm gerekli metodlar zaten var
        // Custom Shipping-specific metodlar gerekirse buraya eklenebilir
    }
}