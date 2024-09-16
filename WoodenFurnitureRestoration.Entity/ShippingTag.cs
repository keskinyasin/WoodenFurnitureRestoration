using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Entities
{
    public class ShippingTag
    {
        public int ShippingId { get; set; }
        public Shipping Shipping { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }


    }

}
