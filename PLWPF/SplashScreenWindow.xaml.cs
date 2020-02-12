using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for SplashScreenWindow.xaml
    /// </summary>
    public partial class SplashScreenWindow : Window
    {
        MainWindow mainwindow = new MainWindow();
        MyBl myBL = new MyBl();
        public SplashScreenWindow()
        {
            InitializeComponent();

            try
            {
                Thread thread = new Thread(myBL.RefreshDatabase);
                thread.Start();
                Thread thread2 = new Thread(fun);
                thread2.Start();
            }
            catch (Exception ex)
            {

                // throw new Exception("Can't get bank info from the web " + ex);
                MessageBox.Show("Fail to refresh the database! \n " + ex.Message);
            }
        }

       public void fun()
        {
            Thread.Sleep(5000);
            Action action=fun2;
           Dispatcher.BeginInvoke(action);
        }

        public void fun2()
        {
            this.Close();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            mainwindow.Show();
        }
    }
}
