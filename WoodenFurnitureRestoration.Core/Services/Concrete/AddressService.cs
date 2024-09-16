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
    public class AddressService : Service<Address>, IAddressRepository
    {
        private readonly AddressRepository _addressRepository;
        
        public AddressService(AddressRepository addressRepository, WoodenFurnitureRestorationContext context)
            : base(addressRepository, context)
        {
            _addressRepository = addressRepository;
        }

        public async Task<List<Address>> GetAddressByCondition(Expression<Func<Address, bool>> expression)
        {
            return await _addressRepository.GetAddressByCondition(expression);
        }
    }
}
