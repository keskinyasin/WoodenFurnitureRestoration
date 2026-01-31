using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Data.Repositories.Abstract
{
    public interface IRestorationServiceRepository : IRepository<RestorationService>
    {
        // Base IRepository<RestorationService>'de tüm gerekli metodlar zaten var
        // Custom RestorationService-specific metodlar gerekirse buraya eklenebilir
    }
}