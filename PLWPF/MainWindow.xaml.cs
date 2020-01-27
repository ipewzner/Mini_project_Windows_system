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
using BE;
using PLWPF.Windows;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Host host = null;

        public MainWindow()
        {
            InitializeComponent();
            //preCode preCode = new preCode();
            //preCode.initialize();
        }

        private void AddRequest_Click(object sender, RoutedEventArgs e)
        {
            //Task t = new Task(() => { });
            Window win = new AddRequestWindow();
                win.Show();

        }

        private void HostingUnit_Click(object sender, RoutedEventArgs e)
        {
            if (host != null)
            {
                Window win = new HostingUnitWindow(host);
            win.Show();
            }
            else MessageBox.Show("Select User Or Sign Up");
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            if (host != null)
            {
                Window win = new OrdersWindow(host);
                win.Show();
            }
            else MessageBox.Show("Select User Or Sign Up");
        }

        private void Stat_Click(object sender, RoutedEventArgs e)
        {
            Window win = new StatisticsWindow();
            win.Show();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogInWindow win = new LogInWindow();
                win.ShowDialog();
                if (win.DialogResult == true)
                {
                    host = win.HostfromLogin;
                    user.Text = host.PrivateName + " " + host.FamilyName;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Select User Or Sign Up");

            }

        }
    }
}
