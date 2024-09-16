using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Entities
{
    public class PaymentTag
    {
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }

}
