using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Data.Repositories.Abstract
{
    public interface ISupplierMaterialRepository : IRepository<SupplierMaterial>
    {

        Task<List<SupplierMaterial>> GetSupplierMaterialsByConditionAsync(Expression<Func<SupplierMaterial, bool>> expression);
          
    }
}
