using PLWPF.Windows;
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
using System.Windows.Shapes;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for OrdersWindow.xaml
    /// </summary>
    public partial class OrdersWindow : Window
    {
        Host host;
        public OrdersWindow(Host host)
        {
            this.host = host;
            InitializeComponent();
        }

        private void UpdateOrder_Click(object sender, RoutedEventArgs e)
        {
            Window win = new UpdateOrderWindow(host);
            win.Show();
        }

        private void NewOrder_Click(object sender, RoutedEventArgs e)
        {
            Window win = new NewOrderWindow(host);
            win.Show();
        }
                            
    }
}
