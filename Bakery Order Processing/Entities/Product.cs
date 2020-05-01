using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery_Order_Processing.Entities
{
    public class Product
    {
        public string productId { get; set; }
        public string productName { get; set; }
        public float productPrice { get; set; }
        public string productDescription { get; set; }
    }
}
