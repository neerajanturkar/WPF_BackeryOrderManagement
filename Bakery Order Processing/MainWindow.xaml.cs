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
            List<String> productNames = new List<string>();
            foreach(Product product in App._products)
            {
                productNames.Add(product.productName);
            }
            Cb_Products.ItemsSource = productNames;
            Cb_Language.ItemsSource = new List<string> { "de", "en" };
            Cb_Language.SelectedItem = language;
        }

        private void Tbx_CustFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filter = (sender as TextBox).Text.ToLower();
            var list = from customer in App._customers where customer.name.ToLower().Contains(filter) select customer;
            Lbx_Customers.ItemsSource = list;
        }

        // Customer 
        private void Btn_AddCustomer_Click(object sender, RoutedEventArgs e)
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
            Tab_Cust_Details.IsSelected = true;
        }

        private void Btn_DelCustomer_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = Lbx_Customers.SelectedItem as Customer;
            if (customer == null)
            {
                MessageBox.Show("Please select a customer to delete", "Famous Bakers", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var res = MessageBox.Show($"Are you sure to delete {customer.name} ?", "Famous Bakers", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res == MessageBoxResult.Yes)
                App._customers.Remove(customer);
        }


        private void Btn_AddCustomerAddress_Click(object sender, RoutedEventArgs e)
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
                Lbx_Addresses.ItemsSource = customer.addresses;
                Lbx_Addresses.SelectedItem = address;
                Lbx_Addresses.ScrollIntoView(address);
            }
        }

        private void Btn_DelCustomerAddress_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = Lbx_Customers.SelectedItem as Customer;
            Address address = Lbx_Addresses.SelectedItem as Address;
            if (address == null)
            {
                MessageBox.Show("Please select an address to delete", "Famous Bakers", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var res = MessageBox.Show($"Are you sure you want to delete {address.title} ?", "Famous Bakers", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res == MessageBoxResult.Yes)
                customer.addresses.Remove(address);
            Lbx_Addresses.ItemsSource = customer.addresses;
        }

        private void Lbx_Customers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCustomer = Lbx_Customers.SelectedItem as Customer;
            if (selectedCustomer != null)
            {
                if (selectedCustomer.addresses != null)
                {
                    Lbx_Addresses.ItemsSource = selectedCustomer.addresses;
                    List<String> _addressTitles = new List<string>();
                    foreach (Address address in selectedCustomer.addresses)
                    {
                        _addressTitles.Add(address.title);
                    }
                    Cb_DeliveryAddress.ItemsSource = _addressTitles;
                }
                if (App._orders != null)
                {
                    ObservableCollection<Order> orderList = new ObservableCollection<Order>(from order in App._orders
                                                                                            where order.custId == selectedCustomer.id
                                                                                            select order);
                    Lbx_Orders.ItemsSource = orderList;

                }
                tblk_street_no.Visibility = Visibility.Hidden;
                tblk_city.Visibility = Visibility.Hidden;
                tblk_country.Visibility = Visibility.Hidden;
            }
        }

        // Order

        private void Btn_AddOrder_Click(object sender, RoutedEventArgs e)
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
                    orderDate = DateTime.Now,
                    deliveryType = "In Store"
                };
                App._orders.Add(newOrder);
                ObservableCollection<Order> orderList = new ObservableCollection<Order>(from order in App._orders
                                                                                        where order.custId == customer.id
                                                                                        select order);
                Lbx_Orders.ItemsSource = orderList;
                Lbx_Orders.SelectedItem = newOrder;
                Lbx_Orders.ScrollIntoView(newOrder);

            }
        }

        private void Btn_CpyOrder_Click(object sender, RoutedEventArgs e)
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
                };
                App._orders.Add(newOrder);
                ObservableCollection<Order> orderList = new ObservableCollection<Order>(from order in App._orders
                                                                                        where order.custId == customer.id
                                                                                        select order);
                Lbx_Orders.SelectedItem = null;
                Lbx_Orders.ItemsSource = orderList;
                Lbx_Orders.SelectedItem = newOrder;
                Lbx_Orders.ScrollIntoView(newOrder);
            }
        }

        private void Btn_DelOrder_Click(object sender, RoutedEventArgs e)
        {
            Order order = Lbx_Orders.SelectedItem as Order;
            if (order == null)
            {
                MessageBox.Show("Please select an order to delete", "Famous Bakers", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var res = MessageBox.Show($"Are you sure you wan to delete order no {order.id} ?", "Famous Bakers", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res == MessageBoxResult.Yes)
            {
                Customer selectedCustomer = Lbx_Customers.SelectedItem as Customer;
                App._orders.Remove(order);
                var orderList = from o in App._orders
                                where o.custId == selectedCustomer.id
                                select o;
                Lbx_Orders.ItemsSource = orderList;
            }

        }

        private void Lbx_Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Order order = Lbx_Orders.SelectedItem as Order;
            if (order != null)
            {
                Order selectedOrder = (from o in App._orders where o.id == order.id select o).FirstOrDefault() as Order;
                if (selectedOrder.deliveryType == "To Location")
                {
                    Rb_ToLocation.IsChecked = true;
                }
                else
                {
                    Rb_InStore.IsChecked = true;
                }
                if (selectedOrder.deliveryAddress != null)
                {
                    if (order.deliveryAddress.title != null)
                        Cb_DeliveryAddress.SelectedItem = order.deliveryAddress.title.ToString();
                }
                if (selectedOrder.orderDate != null)
                {
                    Dp_OrderDate.SelectedDate = order.orderDate;
                }
                if (selectedOrder.orderItems != null)
                {
                    var items = from item in selectedOrder.orderItems
                                select item;
                    Lbx_OrderItems.ItemsSource = items;
                }
                if (DateTime.Now.Subtract(selectedOrder.orderDate).Days > 0)
                {
                    Dp_OrderDate.IsEnabled = false;
                    Cb_DeliveryAddress.IsEnabled = false;
                    Cb_Products.IsEnabled = false;
                    Tbx_quantity.IsEnabled = false;
                    Btn_AddOrderItem.IsEnabled = false;
                    Btn_DelOrderItem.IsEnabled = false;
                }
                else
                {
                    Dp_OrderDate.IsEnabled = true;
                    Cb_DeliveryAddress.IsEnabled = true;
                    Cb_Products.IsEnabled = true;
                    Tbx_quantity.IsEnabled = true;
                    Btn_AddOrderItem.IsEnabled = true;
                    Btn_DelOrderItem.IsEnabled = true;
                }
                CalculateGrandTotal();
            }
        }

        private void Tbx_FilterDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            Customer selectedCustomer = Lbx_Customers.SelectedItem as Customer;
            var filter = (sender as TextBox).Text.ToLower();
            var list = from order in App._orders
                       where order.custId == selectedCustomer.id && String.Format("{0:dd.MM.yyyy  HH:mm:ss}", order.orderDate).Contains(filter)
                       select order;
            Lbx_Orders.ItemsSource = list;
        }

        // Order Item

        private void Btn_AddOrderItem_Click(object sender, RoutedEventArgs e)
        {
            Order selectedOrder = (from order in App._orders where order.id == (Lbx_Orders.SelectedItem as Order).id select order).FirstOrDefault() as Order;
            OrderItem orderItem = new OrderItem
            {
                productName = "Select Item",
                quantity = 0
            };
            selectedOrder.orderItems.Add(orderItem);
            Lbx_OrderItems.ItemsSource = selectedOrder.orderItems;
            Lbx_OrderItems.SelectedItem = orderItem;
            Lbx_OrderItems.ScrollIntoView(orderItem);
        }
        private void Btn_DelOrderItem_Click(object sender, RoutedEventArgs e)
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

        // Order Details

        private void Rb_InStore_Checked(object sender, RoutedEventArgs e)
        {
            Customer selectedCustomer = Lbx_Customers.SelectedItem as Customer;
            lbl_delivery_address.Visibility = Visibility.Hidden;
            Cb_DeliveryAddress.Visibility = Visibility.Hidden;
            tblk_street_no.Visibility = Visibility.Hidden;
            tblk_city.Visibility = Visibility.Hidden;
            tblk_country.Visibility = Visibility.Hidden;

            Order selectedOrder = Lbx_Orders.SelectedItem as Order;
            if (selectedOrder != null)
            {
                Order order = (from o in App._orders where o.id == selectedOrder.id select o).FirstOrDefault() as Order;
                order.deliveryType = "In store";
                order.deliveryAddress = null;
            }
            Lbx_Orders.ItemsSource = from o in App._orders where o.custId == selectedCustomer.id select o;

        }

        private void Rb_ToLocation_Checked(object sender, RoutedEventArgs e)
        {
            Customer selectedCustomer = Lbx_Customers.SelectedItem as Customer;
            lbl_delivery_address.Visibility = Visibility.Visible;
            Cb_DeliveryAddress.Visibility = Visibility.Visible;

            Order selectedOrder = Lbx_Orders.SelectedItem as Order;
            if (selectedOrder != null)
            {

                Order order = (from o in App._orders where o.id == selectedOrder.id select o).FirstOrDefault() as Order;
                order.deliveryType = "To Location";
                if (Cb_DeliveryAddress.SelectedItem != null)
                {
                    var selectedAddress = Cb_DeliveryAddress.SelectedItem.ToString();
                    Address customerAddress = (from a in selectedCustomer.addresses where a.title == selectedAddress select a).FirstOrDefault() as Address;
                    tblk_street_no.Visibility = Visibility.Visible;
                    tblk_city.Visibility = Visibility.Visible;
                    tblk_country.Visibility = Visibility.Visible;
                    tblk_street_no.Text = customerAddress.street + ", " + customerAddress.houseNo;
                    tblk_city.Text = customerAddress.city + ". " + customerAddress.postalCode;
                    tblk_country.Text = customerAddress.country;
                }
                if (Cb_DeliveryAddress.SelectedItem == null)
                {
                    if (selectedCustomer.addresses.Count > 0)
                    {
                        order.deliveryAddress = selectedCustomer.addresses[0];
                        Cb_DeliveryAddress.SelectedItem = selectedCustomer.addresses[0].title;
                        tblk_street_no.Visibility = Visibility.Visible;
                        tblk_city.Visibility = Visibility.Visible;
                        tblk_country.Visibility = Visibility.Visible;
                        tblk_street_no.Text = selectedCustomer.addresses[0].street + ", " + selectedCustomer.addresses[0].houseNo;
                        tblk_city.Text = selectedCustomer.addresses[0].city + ". " + selectedCustomer.addresses[0].postalCode;
                        tblk_country.Text = selectedCustomer.addresses[0].country;
                    }
                }
            }
        }

        private void Cb_DeliveryAddress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Order selectedOrder = Lbx_Orders.SelectedItem as Order;
            Customer customer = Lbx_Customers.SelectedItem as Customer;
            if (Cb_DeliveryAddress.SelectedItem != null)
            {
                var selectedAddress = Cb_DeliveryAddress.SelectedItem.ToString();
                Address customerAddress = (from a in customer.addresses where a.title == selectedAddress select a as Address).FirstOrDefault();
                if (selectedOrder != null)
                {
                    Order order = (from o in App._orders where o.id == selectedOrder.id select o).FirstOrDefault() as Order;
                    order.deliveryAddress = customerAddress;
                    tblk_street_no.Visibility = Visibility.Visible;
                    tblk_city.Visibility = Visibility.Visible;
                    tblk_country.Visibility = Visibility.Visible;
                    tblk_street_no.Text = customerAddress.street + ", " + customerAddress.houseNo;
                    tblk_city.Text = customerAddress.city + ". " + customerAddress.postalCode;
                    tblk_country.Text = customerAddress.country;
                    
                    Lbx_Orders.ItemsSource = from o in App._orders where o.custId == customer.id select o;
                }
            }
        }

        private void Dp_OrderDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Order order = Lbx_Orders.SelectedItem as Order;
            if (Dp_OrderDate.SelectedDate != null)
            {
                if (order != null)
                {
                    order.orderDate = (DateTime)Dp_OrderDate.SelectedDate;
                }
            }
        }

        private void Tbx_quantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculatePrice();
        }

        private void Cb_Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalculatePrice();
        }

        // Common
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Tab_Orders.IsSelected)
            {
                Customer selectedCustomer = Lbx_Customers.SelectedItem as Customer;
                if (selectedCustomer != null)
                {
                    if (selectedCustomer.addresses != null)
                    {

                        Lbx_Addresses.ItemsSource = selectedCustomer.addresses;
                        List<String> _addressTitles = new List<string>();
                        foreach (Address address in selectedCustomer.addresses)
                        {
                            _addressTitles.Add(address.title);
                        }
                        Cb_DeliveryAddress.ItemsSource = _addressTitles;
                    }
                }
            }
        }

        private void Cb_Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (firstTime)
            {
                firstTime = false;
                return;
            }
            else
            {
                language = Cb_Language.SelectedItem.ToString().Substring(0, 2);
                Properties.Settings.Default.language = language;
                Properties.Settings.Default.Save();
                Process.Start(Application.ResourceAssembly.Location);
                App.Current.Shutdown();
            }
        }

        // Menu 
        private void MI_Orders_Click(object sender, RoutedEventArgs e)
        {
            return;
        }

        private void MI_Products_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow productWindow = new ProductWindow();
            productWindow.Show();
            this.Close();
        }

        private void MI_Requirements_Click(object sender, RoutedEventArgs e)
        {
            RequirementsWindow requirementsWindow = new RequirementsWindow();
            requirementsWindow.Show();
            this.Close();
        }


        // General
        private void CalculatePrice()
        {

            if (Cb_Products.SelectedItem != null)
            {
                Product product = (from p in App._products where p.productName == (Cb_Products.SelectedItem).ToString() select p).FirstOrDefault();
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
                            CalculateGrandTotal();
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

        private void CalculateGrandTotal()
        {
            float sum = 0;
            Order order = Lbx_Orders.SelectedItem as Order;
            if (order != null)
            {
                foreach (OrderItem item in order.orderItems)
                {
                    sum += item.productPrice;
                }
                Tblk_GrandTotal.Text = "€ " + sum.ToString();
            }

        }

  
    }
}
