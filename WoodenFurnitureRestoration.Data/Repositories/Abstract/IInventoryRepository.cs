using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Data.Repositories.Abstract
{
    public interface IInventoryRepository : IRepository<Inventory>
    {
        // Base IRepository<Inventory>'de tüm gerekli metodlar zaten var
        // Custom metodlar gerekirse buraya eklenebilir
    }
}