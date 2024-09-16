using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Data.Repositories.Abstract
{
    public interface IReviewRepository : IRepository<Review>
    {

        Task<List<Review>> GetReviewsByConditionAsync(Expression<Func<Review, bool>> expression);
        
    }
}
