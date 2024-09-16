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
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(WoodenFurnitureRestorationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Address>> GetAddressByCondition(Expression<Func<Address, bool>> expression)
        {
            return await _context.Addresses
                .Where(expression)
                .ToListAsync();
        }
    }
}
