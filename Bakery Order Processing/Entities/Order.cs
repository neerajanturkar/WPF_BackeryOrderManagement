using Bakery_Order_Processing.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery_Order_Processing
{
    public class Order
    {
        public string id { get; set; }
        public string custId { get; set; }
        public DateTime orderDate { get; set; }
        public string deliveryType { get; set; }
        public Address deliveryAddress { get; set; }
        public ObservableCollection<OrderItem> orderItems { get; set; }

        public float grandTotal { get; set; }

        public Order()
        {
            orderItems = new ObservableCollection<OrderItem>();
            orderDate = DateTime.Now;
        }
    }
}
