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
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        string language;
        bool firstTime;
        public ProductWindow()
        {
            language = Properties.Settings.Default.language;
            firstTime = true;
            CultureInfo.CurrentCulture = new CultureInfo(language);
            CultureInfo.CurrentUICulture = new CultureInfo(language);
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Lbx_Products.ItemsSource = App._products;
            Cb_Language.ItemsSource = new List<string> { "de", "en" };
            Cb_Language.SelectedItem = language;
        }

        private void MI_Requirements_Click(object sender, RoutedEventArgs e)
        {
            RequirementsWindow requirementsWindow = new RequirementsWindow();
            requirementsWindow.Show();
            this.Close();
        }

        private void MI_Products_Click(object sender, RoutedEventArgs e)
        {
            return;
        }

        private void MI_Orders_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
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

        private void Btn_DelProduct_Click(object sender, RoutedEventArgs e)
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

        private void Btn_AddProduct_Click(object sender, RoutedEventArgs e)
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

        private void Tbx_ProductFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filter = (sender as TextBox).Text.ToLower();
            var list = from product in App._products where product.productName.ToLower().Contains(filter) select product;
            Lbx_Products.ItemsSource = list;
        }

   
    }
}
