using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Data.Repositories.Abstract
{
    public interface IRestorationRepository : IRepository<Restoration>
    {
        // Base IRepository<Restoration>'de tüm gerekli metodlar zaten var
        // Custom Restoration-specific metodlar gerekirse buraya eklenebilir
    }
}