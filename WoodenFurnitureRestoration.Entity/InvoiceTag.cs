using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Entities
{
    public class InvoiceTag
    {
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }

}
