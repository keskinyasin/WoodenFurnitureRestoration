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

        Task<List<Supplier>> GetSuppliersByConditionAsync(Expression<Func<Supplier, bool>> expression);
        
    }
}
