using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BE;
using BL;
using PLWPF.Windows;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
           
        MyBl myBL = new MyBl();
        Host host;
        public Host HostfromLogin { get => host; }
        public LogInWindow()
        {
            InitializeComponent();
            HostComboBox.ItemsSource = myBL.GetHosts();
            HostComboBox.DisplayMemberPath = "FamilyName";

            /*
            HostKey 
            PrivateName  
            FamilyName            
            */

                             
        }

        /// <summary>
        ///  The event selcte the host
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Host_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ENTER_Button.IsEnabled = true;
            ForgetYourPassword.IsEnabled = true;
            host = HostComboBox.SelectedItem as Host;
        }

        /// <summary>
        /// The event call the Register window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Window win = new Register();
            win.Show();
        }

        /// <summary>
        /// The event checkק the password and if it's correct close  the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ENTER_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (myBL.CheckePassword(host.PasswordKey, Int32.Parse(Password.PasswordHidden.Password)))
                {
                    //DialogResult is return to main window
                    DialogResult = true;
                    Close();
                }
                else MessageBox.Show("Password incorrect!\n try again");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Problem with the password \n");
                Console.WriteLine(ex.Message);
            }
        }


        BackgroundWorker worker = new BackgroundWorker();

        private void Worker_SendMailWithNewPassword(object sender, DoWorkEventArgs e)
        {
            myBL.SendMailWithNewPassword(host);
        }

        /// <summary>
        /// The event call to Worker_SendMailWithNewPassword 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ForgetYourPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            worker.DoWork += Worker_SendMailWithNewPassword;
        
            if ( MessageBox.Show("Send you email \nwith new password?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    if (worker.IsBusy != true) worker.RunWorkerAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(""+ ex.Message);
                }
            }
            

        }


      
    }
}
