using Bakery_Order_Processing.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bakery_Order_Processing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string language;
        bool firstTime;
        public MainWindow()
        {
            language = Properties.Settings.Default.language;
            firstTime = true;
            CultureInfo.CurrentCulture = new CultureInfo(language);
            CultureInfo.CurrentUICulture = new CultureInfo(language);
            InitializeComponent();
            
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Lbx_Customers.ItemsSource = App._customers;
            Lbx_Products.ItemsSource = App._products;
            Lbx_Delivery_Dates.ItemsSource = App._orders;
            List<String> productNames = new List<string>();
            foreach(Product product in App._products)
            {
                productNames.Add(product.productName);
            }
            cb_products.ItemsSource = productNames;

            List<String> dates = new List<string>();
            foreach(Order order in App._orders)
            {
                if(!dates.Contains(String.Format("{0:dd.MM.yyyy}", order.orderDate)))
                {
                    dates.Add(String.Format("{0:dd.MM.yyyy}", order.orderDate));
                }
            }
            Lbx_Delivery_Dates.ItemsSource = dates;
            cmb_language.ItemsSource = new List<string> { "de", "en" };
            cmb_language.SelectedItem = language;

        }

        private void tbx_cust_filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filter = (sender as TextBox).Text.ToLower();
            var list = from customer in App._customers where customer.name.ToLower().Contains(filter) select customer;
            Lbx_Customers.ItemsSource = list;
        }

        private void btn_add_customer_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = new Customer
            {
                name = "Edit...",
                lastname = "Edit...",
                firstname = "Edit...",
                phone = "Edit...",
                id = Math.Abs(Guid.NewGuid().GetHashCode()).ToString()
            };
            App._customers.Add(customer);
            Lbx_Customers.SelectedItem = customer;
            Lbx_Customers.ScrollIntoView(customer);
            tab_cust_details.IsSelected = true;

        }

        private void btn_add_customerAddress_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = Lbx_Customers.SelectedItem as Customer;
            if (customer == null)
            {
                MessageBox.Show("Select Customer before adding an address", "Famous Bakers", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Address address = new Address
                {
                    custId = customer.id,
                    addressId = Math.Abs(Guid.NewGuid().GetHashCode()).ToString(),
                    title = "Edit...",
                    street = "Edit...",
                    houseNo = "Edit...",
                    city = "Edit...",
                    country = "Edit...",
                    postalCode = "Edit..."
                };
                customer.addresses.Add(address);
                
                lbx_addresses.ItemsSource = customer.addresses;
                lbx_addresses.SelectedItem = address;
                lbx_addresses.ScrollIntoView(address);

            }

        }

        private void btn_add_order_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = Lbx_Customers.SelectedItem as Customer;
            if (customer == null)
            {
                MessageBox.Show("Select Customer before adding an order", "Famous Bakers", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Order newOrder = new Order
                {
                    id = Math.Abs(Guid.NewGuid().GetHashCode()).ToString(),
                    custId = customer.id,
                    orderDate = DateTime.Now
                };
                App._orders.Add(newOrder);
                var orderList = from order in App._orders
                                  where order.custId == customer.id
                                  select order;
                Lbx_Orders.ItemsSource = orderList;
                Lbx_Orders.SelectedItem = newOrder;
                Lbx_Orders.ScrollIntoView(newOrder);

            }
        }

        private void btn_cpy_order_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = Lbx_Customers.SelectedItem as Customer;
            if (customer == null)
            {
                MessageBox.Show("Select Customer before adding an order", "Famous Bakers", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Order selectedOrder = Lbx_Orders.SelectedItem as Order;
                if (selectedOrder == null)
                {
                    MessageBox.Show("Please select an order to copy", "Famous Bakers", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                Order newOrder = new Order
                {
                    id = Math.Abs(Guid.NewGuid().GetHashCode()).ToString(),
                    custId = selectedOrder.custId,
                    deliveryAddress = selectedOrder.deliveryAddress,
                    deliveryType = selectedOrder.deliveryType,
                    orderItems = selectedOrder.orderItems,
                    orderDate = selectedOrder.orderDate

                };
                App._orders.Add(newOrder);
                var orderList = from order in App._orders
                                where order.custId == customer.id
                                select order;
                Lbx_Orders.ItemsSource = orderList;
                Lbx_Orders.SelectedItem = newOrder;
                Lbx_Orders.ScrollIntoView(newOrder);
            }
        }

        private void btn_del_customer_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = Lbx_Customers.SelectedItem as Customer;
            if(customer == null)
            {
                MessageBox.Show("Please select a customer to delete", "Famous Bakers", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var res = MessageBox.Show($"Are you sure to delete {customer.name} ?", "Famous Bakers", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(res == MessageBoxResult.Yes)
                App._customers.Remove(customer);
        }

        private void btn_del_orderItem_Click(object sender, RoutedEventArgs e)
        {
            Order selectedOrder = Lbx_Orders.SelectedItem as Order;
            OrderItem selectedOrderItem = Lbx_OrderItems.SelectedItem as OrderItem;
            if (selectedOrder == null)
            {
                MessageBox.Show("Please select an order to delete Order Item", "Famous Bakers", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if(selectedOrderItem == null)
                {
                    MessageBox.Show("Please select an order item to delete", "Famous Bakers", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    var res = MessageBox.Show($"Are you sure you want to delete {selectedOrderItem.productName} ?", "Famous Bakers", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (res == MessageBoxResult.Yes)
                    {
                        selectedOrder.orderItems.Remove(selectedOrderItem);
                        Lbx_OrderItems.ItemsSource = selectedOrder.orderItems;
                    }
                        
                }
            }
        }

        private void btn_del_customerAddress_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = Lbx_Customers.SelectedItem as Customer;
            Address address = lbx_addresses.SelectedItem as Address;
            if (address == null)
            {
                MessageBox.Show("Please select an address to delete", "Famous Bakers", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var res = MessageBox.Show($"Are you sure you want to delete {address.title} ?", "Famous Bakers", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res == MessageBoxResult.Yes)
                customer.addresses.Remove(address);
            lbx_addresses.ItemsSource = customer.addresses;
        }

        private void btn_del_order_Click(object sender, RoutedEventArgs e)
        {
            Order order = Lbx_Orders.SelectedItem as Order;
            if(order == null)
            {
                MessageBox.Show("Please select an order to delete", "Famous Bakers", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var res = MessageBox.Show($"Are you sure you wan to delete order no {order.id} ?", "Famous Bakers", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(res == MessageBoxResult.Yes)
            {
                App._orders.Remove(order);
                Lbx_Orders.ItemsSource = App._orders;
            }

        }

        private void Lbx_Customers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var selectedCustomer = Lbx_Customers.SelectedItem as Customer;
            if(selectedCustomer != null) 
            {
                if (selectedCustomer.addresses != null)
                {
                    lbx_addresses.ItemsSource = selectedCustomer.addresses;
                    List<String> _addressTitles = new List<string>();
                    foreach (Address address in selectedCustomer.addresses)
                    {
                        _addressTitles.Add(address.title);
                    }
                    cb_delivery_address.ItemsSource = _addressTitles;
                }
                if (App._orders != null)
                {
                    var orderList = from order in App._orders
                                    where order.custId == selectedCustomer.id
                                    select order;
                    Lbx_Orders.ItemsSource = orderList;

                }
                tblk_street_no.Visibility = Visibility.Hidden;
                tblk_city.Visibility = Visibility.Hidden;
                tblk_country.Visibility = Visibility.Hidden;

            }
            
            
        }

       

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(tab_orders.IsSelected)
            {
                Customer selectedCustomer = Lbx_Customers.SelectedItem as Customer;
                if (selectedCustomer != null)
                {
                    if (selectedCustomer.addresses != null)
                    {
                        
                        lbx_addresses.ItemsSource = selectedCustomer.addresses;
                        List<String> _addressTitles = new List<string>();
                        foreach (Address address in selectedCustomer.addresses)
                        {
                            _addressTitles.Add(address.title);
                        }
                        cb_delivery_address.ItemsSource = _addressTitles;
                    }
                }
            }
        }

        private void rb_in_store_Checked(object sender, RoutedEventArgs e)
        {
            lbl_delivery_address.Visibility = Visibility.Hidden;
            cb_delivery_address.Visibility = Visibility.Hidden;
            tblk_street_no.Visibility = Visibility.Hidden;
            tblk_city.Visibility = Visibility.Hidden;
            tblk_country.Visibility = Visibility.Hidden;

            Order order = Lbx_Orders.SelectedItem as Order;
            if (order != null)
            {
                order.deliveryType = "In store";
                order.deliveryAddress = null;
            }
        }

        private void rb_to_location_Checked(object sender, RoutedEventArgs e)
        {
            Customer selectedCustomer = Lbx_Customers.SelectedItem as Customer;
            lbl_delivery_address.Visibility = Visibility.Visible;
            cb_delivery_address.Visibility = Visibility.Visible;
           
            Order order = Lbx_Orders.SelectedItem as Order;
            if(order != null)
                order.deliveryType = "To Location";
            if(cb_delivery_address.SelectedItem != null)
            {
                var selectedAddress = cb_delivery_address.SelectedItem.ToString();
                Address customerAddress = (from a in selectedCustomer.addresses where a.title == selectedAddress select a as Address).FirstOrDefault();
                tblk_street_no.Visibility = Visibility.Visible;
                tblk_city.Visibility = Visibility.Visible;
                tblk_country.Visibility = Visibility.Visible;
                tblk_street_no.Text = customerAddress.street + ", " + customerAddress.houseNo;
                tblk_city.Text = customerAddress.city + ". " + customerAddress.postalCode;
                tblk_country.Text = customerAddress.country;


            }
        }

        private void Lbx_Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Order order = Lbx_Orders.SelectedItem as Order;
            if (order != null)
            {
                if (order.deliveryType == "To Location")
                {
                    rb_to_location.IsChecked = true;
                }
                else
                {
                    rb_in_store.IsChecked = true;
                }
                if (order.deliveryAddress != null)
                {
                    if(order.deliveryAddress.title != null)
                        cb_delivery_address.SelectedItem = order.deliveryAddress.title.ToString();
                }
                if (order.orderDate != null)
                {
                    dp_order_date.SelectedDate = order.orderDate;
                }
                if(order.orderItems != null)
                {
                    var items = from item in order.orderItems
                                select item;
                    Lbx_OrderItems.ItemsSource = items;
                }
                Calculate_GrandTotal();
            }
        }

        private void cb_delivery_address_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Order order = Lbx_Orders.SelectedItem as Order;
            Customer customer = Lbx_Customers.SelectedItem as Customer;
            if (cb_delivery_address.SelectedItem != null)
            {
                var selectedAddress = cb_delivery_address.SelectedItem.ToString();
                Address customerAddress = (from a in customer.addresses where a.title == selectedAddress select a as Address).FirstOrDefault();
                if (order != null)
                {
                    order.deliveryAddress = customerAddress;
                    tblk_street_no.Visibility = Visibility.Visible;
                    tblk_city.Visibility = Visibility.Visible;
                    tblk_country.Visibility = Visibility.Visible;
                    tblk_street_no.Text = customerAddress.street + ", " + customerAddress.houseNo;
                    tblk_city.Text = customerAddress.city + ". " + customerAddress.postalCode;
                    tblk_country.Text = customerAddress.country;
                }
            }
        }

        private void dp_order_date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Order order = Lbx_Orders.SelectedItem as Order;
            if (dp_order_date.SelectedDate != null)
            {
                if (order != null)
                {
                   order.orderDate = (DateTime)dp_order_date.SelectedDate;
                }
            }
        }

        private void btn_add_orderItem_Click(object sender, RoutedEventArgs e)
        {
            Order order = Lbx_Orders.SelectedItem as Order;
            OrderItem orderItem = new OrderItem
            {
                productName = "Select Item",
                quantity = 0
            };
            order.orderItems.Add(orderItem);
            Lbx_OrderItems.ItemsSource = order.orderItems;
            Lbx_OrderItems.SelectedItem = orderItem;
            Lbx_OrderItems.ScrollIntoView(orderItem);
            


        }

        private void tbx_filter_date_TextChanged(object sender, TextChangedEventArgs e)
        {
            Customer selectedCustomer = Lbx_Customers.SelectedItem as Customer;
            var filter = (sender as TextBox).Text.ToLower();
            var list = from order in App._orders where order.custId == selectedCustomer.id && String.Format("{0:dd.MM.yyyy  HH:mm:ss}", order.orderDate).Contains(filter)
                       select order;
            Lbx_Orders.ItemsSource = list;
        }

        private void btn_del_product_Click(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = Lbx_Products.SelectedItem as Product;
            if (selectedProduct == null)
            {
                MessageBox.Show("Please select a product to delete", "Famous Bakers", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var res = MessageBox.Show($"Are you sure to delete {selectedProduct.productName} ?", "Famous Bakers", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res == MessageBoxResult.Yes)
                App._products.Remove(selectedProduct);
        }

        private void btn_add_product_Click(object sender, RoutedEventArgs e)
        {
            Product newProduct = new Product
            {
                productId = Math.Abs(Guid.NewGuid().GetHashCode()).ToString(),
                productName = "Edit..."
            };
            App._products.Add(newProduct);
            Lbx_Products.ItemsSource = App._products;
            Lbx_Products.SelectedItem = newProduct;
            Lbx_Products.ScrollIntoView(newProduct);
        }

        private void Lbx_Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tbx_product_filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filter = (sender as TextBox).Text.ToLower();
            var list = from product in App._products where product.productName.ToLower().Contains(filter) select product;
            Lbx_Products.ItemsSource = list;
        }

        private void Lbx_Delivery_Dates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String deliveryDate = Lbx_Delivery_Dates.SelectedItem as String;
            var orders = from order in App._orders where String.Format("{0:dd.MM.yyyy}", order.orderDate) == deliveryDate select order;
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach(Order o in orders)
            {
                foreach(OrderItem item in o.orderItems)
                {
                    orderItems.Add(item);
                }
                
            }

          
            
            var res = (from item in orderItems
                      group item by item.productName into g
                      select new
                      {
                          productName = g.Key,
                          sum = g.Sum(i => i.quantity)

                      }).ToList();
            Lbx_Requirements.ItemsSource = res;
            tblk_selected_date.Text = deliveryDate;
        }

        private void Lbx_OrderItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tbx_delivery_date_filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<String> dates = new List<string>();
            var filter = (sender as TextBox).Text.ToLower();
            var list = from order in App._orders
                       where String.Format("{0:dd.MM.yyyy  HH:mm:ss}", order.orderDate).Contains(filter)
                       select order;
            foreach (Order order in list)
            {
                if (!dates.Contains(String.Format("{0:dd.MM.yyyy}", order.orderDate)))
                {
                    dates.Add(String.Format("{0:dd.MM.yyyy}", order.orderDate));
                }
            }
            Lbx_Delivery_Dates.ItemsSource = dates;
        }

        private void cmb_language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (firstTime)
            {
                firstTime = false;
                return;
            }
            else
            {
                language = cmb_language.SelectedItem.ToString().Substring(0, 2);
                Properties.Settings.Default.language = language;
                Properties.Settings.Default.Save();
                Process.Start(Application.ResourceAssembly.Location);
                App.Current.Shutdown();
            }
        }

        private void Tbx_quantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            Calculate_Price();
        }

        private void cb_products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Calculate_Price();
        }

        private void Calculate_Price()
        {
           
            if (cb_products.SelectedItem != null)
            {
                Product product = (from p in App._products where p.productName == (cb_products.SelectedItem).ToString() select p).FirstOrDefault();
                OrderItem orderItem = Lbx_OrderItems.SelectedItem as OrderItem;
                try
                {
                    if (product != null || orderItem != null)
                    {
                        if (Tbx_quantity.Text.Length > 0)
                        {
                            int quantity = Int32.Parse(Tbx_quantity.Text);
                            orderItem.productPrice = product.productPrice * quantity;
                            Tblk_ItemPrice.Text = "€ " + (product.productPrice * quantity).ToString();
                            Calculate_GrandTotal();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Not a number", "Famous Bakers", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            return;
        }

        private void Calculate_GrandTotal()
        {
            float sum = 0;
            Order order = Lbx_Orders.SelectedItem as Order;
            if(order != null)
            {
                foreach (OrderItem item in order.orderItems)
                {
                    sum += item.productPrice;
                }
                Tblk_GrandTotal.Text = "€ " + sum.ToString();
            }
            
        }

        private void MI_Orders_Click(object sender, RoutedEventArgs e)
        {
            Grid_Orders.Visibility = Visibility.Visible;
            Grid_Products.Visibility = Visibility.Hidden;
            Grid_Requirements.Visibility = Visibility.Hidden;
        }

        private void MI_Products_Click(object sender, RoutedEventArgs e)
        {
            Grid_Orders.Visibility = Visibility.Hidden;
            Grid_Products.Visibility = Visibility.Visible;
            Grid_Requirements.Visibility = Visibility.Hidden;
        }

        private void MI_Requirements_Click(object sender, RoutedEventArgs e)
        {
            Grid_Orders.Visibility = Visibility.Hidden;
            Grid_Products.Visibility = Visibility.Hidden;
            Grid_Requirements.Visibility = Visibility.Visible;
        }
    }
}
