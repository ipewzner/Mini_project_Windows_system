using System;
using System.Windows;
using BE;
using BL;
using PLWPF.Windows;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Host host = null;
        MyBl myBL = new MyBl();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddRequest_Click(object sender, RoutedEventArgs e)
        {
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

        private void ShowInfo_Click(object sender, RoutedEventArgs e)
        {
            Window win = new Windows.ShowInfoWindows.ShowInfoWindow();
            win.Show();
        }

        private void editHost_Click(object sender, RoutedEventArgs e)
        {
            Window win = new HostUpdateWindow(host);
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
                    user.Content = host.PrivateName + " " + host.FamilyName;
                    guestButton.Visibility = Visibility.Hidden;
                    logout.Visibility = Visibility.Visible;
                    if (host.HostKey == 00000000)
                    {
                        ShowInfoButton.Visibility = Visibility.Visible;
                        statButton.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        orderButton.Visibility = Visibility.Visible;
                        editUnitButton.Visibility = Visibility.Visible;
                        editHostButton.Visibility = Visibility.Visible;
                        statButton.Visibility = Visibility.Hidden;
  
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Select User Or Sign Up");

            }

        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            host = null;
            user.Content = "";
            guestButton.Visibility = Visibility.Visible;
            orderButton.Visibility = Visibility.Hidden;
            editUnitButton.Visibility = Visibility.Hidden;
            editHostButton.Visibility = Visibility.Hidden;
            statButton.Visibility = Visibility.Hidden;
            ShowInfoButton.Visibility = Visibility.Hidden;
        }
    }
}
