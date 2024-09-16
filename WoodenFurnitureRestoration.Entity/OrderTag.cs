using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Entities
{
    public class OrderTag
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }

}
