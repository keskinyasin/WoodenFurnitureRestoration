using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract
{
    public interface ICustomerService : IService<Customer>
    {
        Task<List<Category>> GetCustomersByFiltersAsync(
                   bool? status = null,
                   string name = null,
                   string description = null
               );
    }
}
