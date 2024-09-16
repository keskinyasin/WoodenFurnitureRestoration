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
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {

        public PaymentRepository(WoodenFurnitureRestorationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Payment>> GetCustomersByConditionAsync(Expression<Func<Payment, bool>> expression)
        {
            return await _context.Payments
                .Where(expression)
                .ToListAsync();
        }
    }
}
