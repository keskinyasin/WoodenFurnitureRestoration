using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Entities
{
    public class SupplierMaterialTag
    {

        public int SupplierMaterialId { get; set; }
        public SupplierMaterial SupplierMaterial { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }

    }
}
