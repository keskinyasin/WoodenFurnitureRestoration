using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Entities
{
    public class ShippingPayment
    {
        public int ShippingId { get; set; }
        public Shipping Shipping { get; set; }

        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}
