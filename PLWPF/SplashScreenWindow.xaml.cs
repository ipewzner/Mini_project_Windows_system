using BL;
using System;
using System.Threading;
using System.Windows;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for SplashScreenWindow.xaml
    /// </summary>
    public partial class SplashScreenWindow : Window
    {
        Thread thread;
        MainWindow mainwindow = new MainWindow();
        MyBl myBL = new MyBl();

        /// <summary>
        /// c-tor
        /// </summary>
        public SplashScreenWindow()
        {
            InitializeComponent();

            try
            {
                thread = new Thread(doWork) { IsBackground = true }; 
                thread.Start();
            }
            catch (Exception ex){MessageBox.Show("" + ex.Message);}
        }


        /// <summary>
        ///  Refresh the Database and close the window when it finise or when 10 sec pass, the leteset 
        /// </summary>
        public void doWork()
        {
            Action closingTheWindow =()=> this.Close();
            Action RefreshingInfoLabel = () => InfoLabel.Content = "Initialize";

            TimeSpan dalta = new TimeSpan(0, 0, 0, 5);
            DateTime bagin = DateTime.Now;
            try { myBL.RefreshDatabase(); }
            catch (Exception ex) { throw new Exception(""+ex); }
            finally
            {
                Dispatcher.BeginInvoke(RefreshingInfoLabel);
               
                //Wait for a  the minimel time of 10 sec
                while (DateTime.Now - bagin < dalta) { }
                Dispatcher.BeginInvoke(closingTheWindow);
            }

        }

        /// <summary>
        /// Window Closed event thet show the main window and handle the progres bar (it not properly without it)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            thread.Abort();
            mainwindow.Show();
            Progress.IsIndeterminate = false;
        }

        /// <summary>
        /// handle the progres bar (it not work without it)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Progress.Visibility = Visibility.Visible;
            Progress.IsIndeterminate = true;
        }
    }
}
 