using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Data.DbContextt;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WoodenFurnitureRestoration.Data.Repositories.Concrete
{
    public class RestorationRepository : Repository<Restoration>, IRestorationRepository
    {
        public RestorationRepository(WoodenFurnitureRestorationContext context) : base(context)
        {
            _context = context;
        }

        // Base Repository metodları yeterli
    }
}