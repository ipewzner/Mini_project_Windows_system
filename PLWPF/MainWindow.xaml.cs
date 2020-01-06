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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddRequest_Click(object sender, RoutedEventArgs e)
        {
            //Task t = new Task(() => { });
            Window win = new AddRequestWindow();
                win.Show();

        }

        private void HostingUnit_Click(object sender, RoutedEventArgs e)
        {
            Window win = new HostingUnitWindow();
            win.Show();
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            Window win = new OrdersWindow();
            win.Show();
        }

        private void Stat_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
