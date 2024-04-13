using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace Prac3._2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OnlineShopEntities context = new OnlineShopEntities();
        private ObservableCollection<CombinedData> combinedDataList = new ObservableCollection<CombinedData>();
        public MainWindow()
        {
            InitializeComponent();

            var orders = context.Orders.ToList();
            var customers = context.Customers.ToList();
            var products = context.Products.ToList();

            foreach (var order in orders)
            {
                var customer = customers.FirstOrDefault(c => c.CustomerID == order.CustomerID);
                var product = products.FirstOrDefault(p => p.ProductID == order.ProductID);

                if (customer != null && product != null)
                {
                    combinedDataList.Add(new CombinedData
                    {
                        Quantity = order.OrdersQuantity.Value,
                        OrderDate = order.OrderDate.Value,
                        CustomerName = customer.Name,
                        CustomerEmail = customer.Email,
                        ProductName = product.Name,
                        ProductPrice = product.Price.Value
                    });
                }

                CombinedDataGrid.ItemsSource = combinedDataList;

            }
        }
        public class CombinedData
        {
            public int Quantity { get; set; }
            public DateTime OrderDate { get; set; }
            public string CustomerName { get; set; }
            public string CustomerEmail { get; set; }
            public string ProductName { get; set; }
            public decimal ProductPrice { get; set; }
        }
    }
}