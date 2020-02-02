using BE;
using BL;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF.Windows.Statistics
{
    /// <summary>
    /// Interaction logic for OrdersQueryPage.xaml
    /// </summary>
    public partial class OrdersQueryPage : Page
    {
        MyBl myBL = new MyBl();

        public OrdersQueryPage()
        {
            InitializeComponent();
            OrderStatus_ComboBox.ItemsSource = Enum.GetValues(typeof(BE.OrderStatus)).Cast<BE.OrderStatus>();
            averageOrdersPerClient_TextBox.Text      = myBL.averageOrdersPerClient().ToString();
            averageOrdersPerHostingUnit_TextBox.Text = myBL.averageOrdersPerHostingUnit().ToString();
        }

        private void OrderStatus_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrderStatus_TextBox.Text = myBL.GetOrders(x=>x.Status == (OrderStatus)OrderStatus_ComboBox.SelectedItem).Count().ToString();
        }

    }
}

