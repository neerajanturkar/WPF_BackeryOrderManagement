using Bakery_Order_Processing.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Bakery_Order_Processing
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ObservableCollection<Customer> _customers;
        public static ObservableCollection<Order> _orders;
        public static ObservableCollection<Product> _products = new ObservableCollection<Product>();

        public App() { }

        public void Application_Startup(object sender, StartupEventArgs e)
        {
            _customers = FileHandler.ReadXML<ObservableCollection<Customer>>("customers.xml");
            _orders = FileHandler.ReadXML<ObservableCollection<Order>>("orders.xml");
            _products = FileHandler.ReadXML<ObservableCollection<Product>>("products.xml");
            if(_customers == null)
            {
                _customers = new ObservableCollection<Customer>();
            }


            if (_orders == null)
            {
                _orders = new ObservableCollection<Order>();
            }

            if (_products == null)
            {
                _products = new ObservableCollection<Product>();
              
            }
        }

       
        public void Application_Exit(object sender, ExitEventArgs e)
        {

            FileHandler.WriteXML<ObservableCollection<Customer>>(_customers, "customers.xml");
            FileHandler.WriteXML<ObservableCollection<Order>>(_orders, "orders.xml");
            FileHandler.WriteXML<ObservableCollection<Product>>(_products, "products.xml");
          
        }
    }
}
