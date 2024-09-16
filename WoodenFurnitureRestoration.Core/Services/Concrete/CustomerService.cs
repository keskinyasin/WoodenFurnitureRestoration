using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.DbContextt;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Concrete;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete
{
    public class CustomerService : Service<Customer>, ICustomerService, ICustomerRepository
    {
        private readonly CustomerRepository _customerRepository;
        private readonly WoodenFurnitureRestorationContext _context;

        public CustomerService(CustomerRepository customerRepository, WoodenFurnitureRestorationContext context) : base(customerRepository, context)
        {
            _customerRepository = customerRepository;
            _context = context;
        }

        public async Task<List<Customer>> GetCustomersByAddressAndRestorationAsync(string City, string District, string Country, int RestorationId)
        {
            return await _customerRepository.GetCustomersByAddressAndRestorationAsync(City, District, Country, RestorationId);
        }

        public async Task<List<Customer>> GetCustomersByConditionAsync(Expression<Func<Customer, bool>> expression)
        {
            return await _customerRepository.GetCustomersByConditionAsync(expression);
        }

        public async Task<List<Category>> GetCustomersByFiltersAsync(bool? status = null, string name = null, string description = null)
        {
            return await _customerRepository.GetCustomersByFiltersAsync(status, name, description);
        }
    }
}
