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
    public class InventoryService : Service<Inventory>, IInventoryService, IInventoryRepository
    {
        private readonly InventoryRepository _inventoryRepository;
        private readonly WoodenFurnitureRestorationContext _context;

        public InventoryService(InventoryRepository inventoryRepository, WoodenFurnitureRestorationContext context) : base(inventoryRepository, context)
        {
            _inventoryRepository = inventoryRepository;
            _context = context;
        }

        public async Task<List<Inventory>> GetInventoriesByConditionAsync(Expression<Func<Inventory, bool>> expression)
        {
            return await _inventoryRepository.GetInventoriesByConditionAsync(expression);
        }
    }
}
