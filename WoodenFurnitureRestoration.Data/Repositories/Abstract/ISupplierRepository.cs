using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Data.Repositories.Abstract
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        // Base IRepository<Supplier>'de tüm gerekli metodlar zaten var
        // Custom Supplier-specific metodlar gerekirse buraya eklenebilir
    }
}