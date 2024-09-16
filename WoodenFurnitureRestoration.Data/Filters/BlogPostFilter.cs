using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Data.Filters
{
    public class BlogPostFilter
    {
        public AddressFilter Address { get; set; }
        public Guid? TagId { get; set; }
        public bool IncludeRating { get; set; }
        public bool IncludeCategory { get; set; }
        public Guid CategoryId { get; set; }
    }
}
