using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Data.Repositories.Abstract
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        // Base IRepository<Payment>'de tüm gerekli metodlar zaten var
        // Custom Payment-specific metodlar gerekirse buraya eklenebilir
    }
}