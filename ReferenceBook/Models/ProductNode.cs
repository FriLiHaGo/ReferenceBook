using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceBook
{
    public class ProductNode
    {
        public string VendorCode { get; set; }

        public string Brand { get; set; }

        public IEnumerable<Product> Analogues { get; set; }
    }
}
