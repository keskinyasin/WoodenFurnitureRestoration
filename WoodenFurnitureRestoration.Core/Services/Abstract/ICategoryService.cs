using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract
{
    public interface ICategoryService : IService<Category>
    {
        Task<List<Category>> GetCategoriesByFiltersAsync(
                   bool? status = null,
                   string name = null,
                   string description = null
               );
    }
}
