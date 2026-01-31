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
        // Base IRepository<Review>'de tüm gerekli metodlar zaten var
        // Custom Review-specific metodlar gerekirse buraya eklenebilir
    }
}