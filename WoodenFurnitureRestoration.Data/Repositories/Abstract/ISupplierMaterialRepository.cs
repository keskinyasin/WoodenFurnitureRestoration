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
        // Base IRepository<SupplierMaterial>'de tüm gerekli metodlar zaten var
        // Custom SupplierMaterial-specific metodlar gerekirse buraya eklenebilir
    }
}