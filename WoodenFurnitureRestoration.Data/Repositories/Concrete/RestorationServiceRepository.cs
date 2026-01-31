using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Data.DbContextt;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Data.Repositories.Concrete
{
    public class RestorationServiceRepository : Repository<RestorationService>, IRestorationServiceRepository
    {
        public RestorationServiceRepository(WoodenFurnitureRestorationContext context) : base(context)
        {
            _context = context;
        }

        // Base Repository metodları yeterli
    }
}