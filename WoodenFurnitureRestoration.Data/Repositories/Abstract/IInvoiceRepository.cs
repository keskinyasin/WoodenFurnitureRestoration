using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Data.Repositories.Abstract
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        // Base IRepository<Invoice>'de tüm gerekli metodlar zaten var
        // Custom Invoice-specific metodlar gerekirse buraya eklenebilir
    }
}