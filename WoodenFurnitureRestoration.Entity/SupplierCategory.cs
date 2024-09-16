using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Entities
{
    public class SupplierCategory
    {
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
