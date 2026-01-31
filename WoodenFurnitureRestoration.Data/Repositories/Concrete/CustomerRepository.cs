using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;
using WoodenFurnitureRestoration.Data.DbContextt;
using Microsoft.EntityFrameworkCore;

namespace WoodenFurnitureRestoration.Data.Repositories.Concrete
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(WoodenFurnitureRestorationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetCustomersByAddressAndRestorationAsync(string city, string district, string country, int restorationId)
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(c => c.Addresses.Any(a => a.City == city));
            }

            if (!string.IsNullOrEmpty(district))
            {
                query = query.Where(c => c.Addresses.Any(a => a.District == district));
            }

            if (!string.IsNullOrEmpty(country))
            {
                query = query.Where(c => c.Addresses.Any(a => a.Country == country));
            }

            query = query.Where(c => c.Orders.Any(o => o.OrderDetails.Any(od => od.RestorationId == restorationId)));

            return await query
                .Distinct()
                .ToListAsync();
        }
    }
}