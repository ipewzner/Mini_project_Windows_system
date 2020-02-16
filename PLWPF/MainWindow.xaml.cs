using System;
using System.Threading;
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
        /// <summary>
        /// c-tor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent ();
           
        }

        /// <summary>
        /// AddRequest_Click event call AddRequestWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddRequest_Click(object sender, RoutedEventArgs e)
        {
            Window win = new AddRequestWindow();
            win.Show();
        }

        /// <summary>
        /// HostingUnit_Click event call HostingUnitWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HostingUnit_Click(object sender, RoutedEventArgs e)
        {
            if (host != null)
            {
                Window win = new HostingUnitWindow(host,this);
                win.Show();
            }
            else MessageBox.Show("Select User Or Sign Up");
        }

        /// <summary>
        /// Orders_Click event call OrdersWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            if (host != null)
            {
                Window win = new OrdersWindow(host);
                win.Show();
            }
            else MessageBox.Show("Select User Or Sign Up");
        }

        /// <summary>
        /// Stat_Click event call StatisticsWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Stat_Click(object sender, RoutedEventArgs e)
        {
            Window win = new StatisticsWindow();
            win.Show();
        }

        /// <summary>
        /// ShowInfo_Click event call ShowInfoWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowInfo_Click(object sender, RoutedEventArgs e)
        {
            Window win = new Windows.ShowInfoWindows.ShowInfoWindow();
            win.Show();
        }

        /// <summary>
        /// editHost_Click event call HostUpdateWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editHost_Click(object sender, RoutedEventArgs e)
        {
            HostUpdateWindow win = new HostUpdateWindow(host,this);
            win.ShowDialog();

            if (win.DialogResult == true)
            {
                if (win.HostDeleted)
                    LogOut();
            }
        }
        /// <summary>
        /// log out and handle visbility acording
        /// </summary>
        public void LogOut() {
            host = null;
            user.Content = "";
            guestButton.Visibility = Visibility.Visible;
            orderButton.Visibility = Visibility.Hidden;
            editUnitButton.Visibility = Visibility.Hidden;
            editHostButton.Visibility = Visibility.Hidden;
            //statButton.Visibility = Visibility.Hidden;
            ShowInfoButton.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Button_Click event call LogInWindow and handle visibility of some window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogIn_Click(object sender, RoutedEventArgs e)
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
                        orderButton.Visibility = Visibility.Hidden;
                        editUnitButton.Visibility = Visibility.Hidden;
                        editHostButton.Visibility = Visibility.Hidden;
                        ShowInfoButton.Visibility = Visibility.Visible;
                        //statButton.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        ShowInfoButton.Visibility = Visibility.Hidden;
                        //statButton.Visibility = Visibility.Hidden;
                        orderButton.Visibility = Visibility.Visible;
                        editUnitButton.Visibility = Visibility.Visible;
                        editHostButton.Visibility = Visibility.Visible;
                       // statButton.Visibility = Visibility.Hidden;

                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Select User Or Sign Up");

            }

        }

        /// <summary>
        /// LogOut_Click event logOut and change visibility of some windows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            LogOut();
        }

        /// <summary>
        /// When the main window closed, exit the environment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
    }
}
