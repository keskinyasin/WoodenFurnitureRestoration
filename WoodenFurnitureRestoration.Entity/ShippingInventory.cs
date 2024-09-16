using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Entities
{
    public class ShippingInventory
    {
        public int ShippingId { get; set; }
        public Shipping Shipping { get; set; }

        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
    }
}
