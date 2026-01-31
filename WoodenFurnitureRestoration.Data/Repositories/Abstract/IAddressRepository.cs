using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Data.Repositories.Abstract
{
    public interface IAddressRepository : IRepository<Address>
    {
        // Address-specific custom methods sadece buraya gelir
        // Şu an custom metod gerekmiyorsa boş kalabilir
    }
}