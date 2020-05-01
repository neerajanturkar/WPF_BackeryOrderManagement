using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery_Order_Processing
{
    public class Customer
    {
        public string id { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string phone { get; set; }
        public ObservableCollection<Address> addresses { get; set; }

        public Customer()
        {
            addresses = new ObservableCollection<Address>();
        }

    }
}
