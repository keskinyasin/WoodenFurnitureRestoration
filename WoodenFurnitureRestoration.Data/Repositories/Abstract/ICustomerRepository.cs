using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Data.Repositories.Abstract
{
    public interface ICustomerRepository : IRepository<Customer>
    {

        Task<List<Customer>> GetCustomersByConditionAsync(Expression<Func<Customer, bool>> expression);

        Task<List<Customer>> GetCustomersByAddressAndRestorationAsync(string City, string District, string Country, int RestorationId);

    }
}
