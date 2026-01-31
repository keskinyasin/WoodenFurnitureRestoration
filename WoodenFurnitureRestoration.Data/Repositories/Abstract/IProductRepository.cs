using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Data.Repositories.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        // Base IRepository<Product>'de tüm gerekli metodlar zaten var
        // Custom Product-specific metodlar gerekirse buraya eklenebilir
    }
}