using Bakery_Order_Processing.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bakery_Order_Processing
{
    /// <summary>
    /// Interaction logic for RequirementsWindow.xaml
    /// </summary>
    public partial class RequirementsWindow : Window
    {

        string language;
        bool firstTime;
        public RequirementsWindow()
        {
            language = Properties.Settings.Default.language;
            firstTime = true;
            CultureInfo.CurrentCulture = new CultureInfo(language);
            CultureInfo.CurrentUICulture = new CultureInfo(language);
            InitializeComponent();
        }

        

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Cb_Language.ItemsSource = new List<string> { "de", "en" };
            Cb_Language.SelectedItem = language;
            List<String> dates = new List<string>();
            foreach (Order order in App._orders)
            {
                if (!dates.Contains(String.Format("{0:dd.MM.yyyy}", order.orderDate)))
                {
                    dates.Add(String.Format("{0:dd.MM.yyyy}", order.orderDate));
                }
            }
            Lbx_ProductionDates.ItemsSource = dates;
        }

        private void Tbx_ProductionDateFilter_TextChanged(object sender, TextChangedEventArgs e)
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
            Lbx_ProductionDates.ItemsSource = dates;
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

        private void Lbx_ProductionDates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String deliveryDate = Lbx_ProductionDates.SelectedItem as String;
            var orders = from order in App._orders where String.Format("{0:dd.MM.yyyy}", order.orderDate) == deliveryDate select order;
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (Order o in orders)
            {
                foreach (OrderItem item in o.orderItems)
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
            Tblk_SelectedDate.Text = deliveryDate;
        }

        private void MI_Orders_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void MI_Products_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow productWindow = new ProductWindow();
            productWindow.Show();
            this.Close();
        }

        private void MI_Requirements_Click(object sender, RoutedEventArgs e)
        {
            return;
        }
    }
}
